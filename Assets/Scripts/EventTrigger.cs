using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] StalkerIA[] Zombies;
    [SerializeField] Door[] Doors;
    [SerializeField] Animator Anim;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Anim.SetTrigger("Entrar");
        }
    }

    void Action()
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].Key(true);
            Doors[i].Active();
        }
        for (int i = 0; i < Zombies.Length; i++)
        {
            Zombies[i].state = EMobState.Chasing;
        } 
    }

    void Exit()
    {
        Destroy(gameObject);
    }
}
