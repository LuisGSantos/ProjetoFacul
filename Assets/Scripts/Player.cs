using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;
    public int minDmg;

    [SerializeField] PMove _Pmove;

    private void Start()
    {
        _Pmove = GetComponent<PMove>();
        CurrentHealth = MaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AreaDano"))
        {
            CurrentHealth -= minDmg;
            _Pmove.enabled = false;
        }
        else
            _Pmove.enabled = true;

    }
}
