using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EMobState { Idle, Attacking, Chasing, moveAway, ReactHit, Seeyou};

public class StalkerIA : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] StalkerIA IA;
    [SerializeField] FovEnemy Head;
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] public Animator animator;
    public EMobState state;
    [SerializeField] Transform target;
    [SerializeField] float Distance;
    [SerializeField] Vector3 iniPosition;
    [SerializeField] float cooldown;
    [SerializeField] public float Life = 10;
    [SerializeField] Rigidbody[] AllRig;
    [SerializeField] Collider[] Hands;
    public enum TypeIni
    {
        Deitado,Sentado,Comendo,EmPe
    }
    public TypeIni Inicio = TypeIni.Sentado;

    void Start()
    {
        Ragdoll(false);
        float rand = Random.Range(0.8f, 0.9f);
        transform.localScale = new Vector3(rand,rand,rand);
        iniPosition = transform.position;
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        if (Inicio == TypeIni.Deitado)
        {
            animator.SetInteger("TypeIni", 0);
        }
        else if (Inicio == TypeIni.Sentado)
        {
            animator.SetInteger("TypeIni", 1);
        }
        else if (Inicio == TypeIni.Comendo)
        {
            animator.SetInteger("TypeIni", 2);
        }
        else if (Inicio == TypeIni.EmPe)
        {
            animator.SetInteger("TypeIni", 3);
        }
    }

    void FixedUpdate()
    {
        Die();
        if (Life > 0)
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            target = GameObject.FindGameObjectWithTag("Player").transform;
            Distance = Vector3.Distance(transform.position, target.position);
            animator.SetFloat("Speed", navAgent.velocity.magnitude);
            SetState(state);
        }
    }

    public void Return()
    {
        state = EMobState.Chasing;
    }

    void SetState(EMobState _state)
    {

        
        state = _state;

        switch (state)
        {
            default:
            case EMobState.Idle:
                for (int i = 0; i <= 1; i++)
                {
                    Hands[i].enabled = false;
                }
                navAgent.isStopped = true;
                if (Head.inimigosVisiveis.Count > 0 || Distance <= 3f)
                {
                    state = EMobState.Chasing;
                }   
                else
                    state = EMobState.Idle;
                break;
            case EMobState.Chasing:
                for (int i = 0; i <= 1; i++)
                {
                    Hands[i].enabled = false;
                }
                navAgent.SetDestination(target.position);
                RaycastHit hit;
                Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, 2f);
                Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward * 2, Color.blue);
                if (hit.collider != null)
                {
                    int Rand = Random.Range(0, 10);
                    if(Head.inimigosVisiveis.Count == 0)
                    {
                        if (hit.collider.gameObject.CompareTag("Door") && Rand != 1)
                        {
                            Debug.Log("Porta na frente");
                            state = EMobState.Idle;
                        }
                        else if (hit.collider.gameObject.CompareTag("Door") && Rand == 1 && !hit.collider.gameObject.GetComponent<Door>().Locked)
                        {
                            hit.collider.gameObject.GetComponent<Door>().aniDoor.SetBool("Open", true);
                        }
                        else
                        {
                            Debug.Log("Nada na frente");
                        }
                    }
                }
                if (cooldown > 0)
                {
                    navAgent.isStopped = true;
                }
                else
                    navAgent.isStopped = false;

                if (Distance <= 2f && Head.inimigosVisiveis.Count > 0)
                {
                    state = EMobState.Attacking;
                }
                else if(Head.inimigosVisiveis.Count <= 0 && Distance >= 25f)
                    state = EMobState.moveAway;
                break;
            case EMobState.moveAway:
                for (int i = 0; i <= 1; i++)
                {
                    Hands[i].enabled = false;
                }
                navAgent.isStopped = false;
                navAgent.destination = iniPosition;
                if (Head.inimigosVisiveis.Count > 0)
                    state = EMobState.Chasing;
                else
                    state = EMobState.moveAway;
                if (transform.position == iniPosition)
                    state = EMobState.Idle;
                break;
            case EMobState.Attacking:
                navAgent.isStopped = true;
                Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, 2f);
                Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward * 2, Color.red);
                if (cooldown <= 0 && hit.collider.gameObject.CompareTag("Player"))
                {
                    animator.SetTrigger("Attack");
                    cooldown = 1f;
                }
                break;
            case EMobState.ReactHit:
                navAgent.isStopped = true;
                if (cooldown <= 0)
                {
                    
                    state = EMobState.Chasing;
                }
                break;
        }
    }

    public void HitProjectile()
    {
        state = EMobState.ReactHit;
        cooldown = 1f;
        animator.SetTrigger("Hit");
    }

    public void AnimHitinTrigger()
    {
        for (int i = 0; i <= 1; i++)
        {
            Hands[i].enabled = true;
        }
    }

    void Die()
    {
        if (Life <= 0)
        {
            Destroy(animator);
            Destroy(navAgent);
            Destroy(GetComponent<BoxCollider>());
            Ragdoll(true);
            if(GetComponent<DropItens>())
            {
                GetComponent<DropItens>().Drop();
                Destroy(GetComponent<DropItens>());
            }
            Destroy(IA);
        }
    }

    public void Ragdoll(bool Active)
    {
        if(Active)
        {
            //navAgent.isStopped = true;
            //animator.enabled = false;
            for (int i = 0; i <= 10; i++)
            {
                AllRig[i].isKinematic = false;
            }
        }
        else
        {
            animator.enabled = true;
            for (int i = 0; i <= 10; i++)
            {
                AllRig[i].isKinematic = true;
            }
        }
    }
}

