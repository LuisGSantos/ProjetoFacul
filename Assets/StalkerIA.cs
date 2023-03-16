using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EMobState { Idle, Attacking, Chasing, moveAway};

public class StalkerIA : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] Animator animator;
    public EMobState state;
    [SerializeField] Transform target;
    [SerializeField] float Distance;
    [SerializeField] Vector3 iniPosition;
    [SerializeField]float cooldown;

    void Start()
    {
        iniPosition = transform.position;
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Distance = Vector3.Distance(transform.position, target.position);
        animator.SetFloat("Speed", navAgent.velocity.magnitude);
        SetState(state);
    }

    public void Return()
    {
        state = EMobState.Chasing;
    }

    void SetState(EMobState _state)
    {

        RaycastHit hit;
        state = _state;

        switch (state)
        {
            default:
            case EMobState.Idle:
                navAgent.isStopped = true;
                
                Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, 3);
                if (hit.collider != null)
                    if(hit.collider.CompareTag("Player"))
                        state = EMobState.Chasing;
                    else
                        state = EMobState.Idle;
                else
                    state = EMobState.Idle;
                break;
            case EMobState.Chasing:
                navAgent.isStopped = false;
                Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, 1);
                navAgent.SetDestination(target.position);
                if (hit.collider != null)
                    if (hit.collider.CompareTag("Door"))
                        state = EMobState.moveAway;
                    else
                        state = EMobState.Chasing;
                else
                    state = EMobState.Chasing;
                if (Distance <= 1.5f)
                    state = EMobState.Attacking;
                break;
            case EMobState.moveAway:
                navAgent.isStopped = false;
                navAgent.destination = iniPosition;
                Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, 2);
                if (hit.collider != null)
                    if (hit.collider.CompareTag("Player"))
                        state = EMobState.Chasing;
                    else if(hit.collider.CompareTag("Door"))
                        state = EMobState.Idle;
                else
                    state = EMobState.moveAway;

                if (transform.position == iniPosition)
                    state = EMobState.Idle;
                break;
            case EMobState.Attacking:
                navAgent.isStopped = true;
                if(cooldown <= 0)
                {
                    animator.SetTrigger("Attack");
                    cooldown = 3f;
                }
                break;
        }
    }
}
