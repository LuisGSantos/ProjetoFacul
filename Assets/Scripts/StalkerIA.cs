using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EMobState { Idle, Attacking, Chasing, moveAway, ReactHit,Seeyou};

public class StalkerIA : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] StalkerIA IA;
    [SerializeField] FovEnemy Head;
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] Animator animator;
    public EMobState state;
    [SerializeField] Transform target;
    [SerializeField] float Distance;
    [SerializeField] Vector3 iniPosition;
    [SerializeField] float cooldown;
    [SerializeField] float Life = 10;
    [SerializeField] Rigidbody[] AllRig;
    [SerializeField] Collider AreaHit;
    public enum TypeIni
    {
        Deitado,Sentado,Comendo
    }
    public TypeIni Inicio = TypeIni.Sentado;

    void Start()
    {
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
                navAgent.isStopped = true;
                if (Head.inimigosVisiveis.Count > 0)
                {
                    state = EMobState.Chasing;
                }   
                else
                    state = EMobState.Idle;
                break;
            case EMobState.Chasing:
                AreaHit.enabled = false;
                navAgent.isStopped = false;
                navAgent.SetDestination(target.position);
                RaycastHit hit;
                Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, 2f);
                Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward * 2, Color.blue);
                if (hit.collider != null)
                {
                    int Rand = Random.Range(0, 10);
                    if (hit.collider.gameObject.CompareTag("Door") && Rand != 1)
                    {
                        Debug.Log("Porta na frente");
                        state = EMobState.Idle;
                    }
                    else if(hit.collider.gameObject.CompareTag("Door") && Rand == 1 && !hit.collider.gameObject.GetComponent<Door>().Locked)
                    {
                        hit.collider.gameObject.GetComponent<Door>().aniDoor.SetBool("Open",true);
                    }
                    else
                    {
                        Debug.Log("Nada na frente");
                    }
                }
                else if (Distance <= 1.5f && Head.inimigosVisiveis.Count > 0)
                    state = EMobState.Attacking;
                else if(Head.inimigosVisiveis.Count <= 0 && Distance >= 8f)
                    state = EMobState.moveAway;
                break;
            case EMobState.moveAway:
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
                if (cooldown <= 0)
                {
                    animator.SetTrigger("Attack");
                    AreaHit.enabled = true;
                    cooldown = 2f;
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Life > 0)
        {
            if (other.CompareTag("Melee"))
            {
                Debug.Log("Acertou");
                animator.SetTrigger("Hit");
                navAgent.isStopped = true;
                state = EMobState.Chasing;
                Life -= 2;
            }
        }
    }

    void Die()
    {
        if (Life <= 0)
        {
            Destroy(animator);
            Destroy(navAgent);
            Destroy(GetComponent<BoxCollider>());
            Destroy(AreaHit);
            for (int i = 0; i <= 10; i++)
            {
                AllRig[i].isKinematic = false;
            }
            if(GetComponent<DropItens>())
            {
                GetComponent<DropItens>().Drop();
                Destroy(GetComponent<DropItens>());
            }
            Destroy(IA);
        }
    }
}

