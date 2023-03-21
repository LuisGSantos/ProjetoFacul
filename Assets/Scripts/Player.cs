using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;
    public int minDmg;
    public float time;

    [SerializeField] PMove _Pmove;

    private void Start()
    {
        _Pmove = GetComponent<PMove>();
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AreaDano"))
        {
            time += Time.deltaTime;
            _Pmove.enabled = false;
            if (time < 1f && Input.GetKeyDown(KeyCode.Space))
            {
                CurrentHealth -= 1;
            }
            else
                CurrentHealth -= 5;
        }
        else
            _Pmove.enabled = true;

    }
}
