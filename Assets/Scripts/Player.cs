using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;
    public int minDmg;
    public float time;
    public float Cooldown;
    public GameObject Head;
    public PMove Pmove;

    [SerializeField] MainControl Main;

    [SerializeField] CharacterController Character;

    private void Start()
    {
        Character = GetComponent<CharacterController>();
        Main = GameObject.Find("Canvas").GetComponent<MainControl>();
        CurrentHealth = MaxHealth;
        
    }

    private void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime; 
        }
        if(Pmove.enabled == true)
        {
            Die();
        }
        if(CurrentHealth <= 0)
        {
            if(time <= 0)
            {
                Main.Restart();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyHand"))
        {
            if(time <= 0)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, -transform.forward,out hit,2f);
                if (hit.collider == null)
                {
                    Character.Move(-transform.forward*0.2f);
                }
                Character.enabled = false;
                Dmg();
            }
        }
        else if(other.CompareTag("Fire"))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, -transform.forward, out hit, 2f);
            if (hit.collider == null)
            {
                Character.Move(-transform.forward * 1.5f);
            }
            CurrentHealth -= 2;
        }
        else
            Character.enabled = true;
    }

    void Dmg()
    {
        CurrentHealth -= minDmg;
        Debug.Log("Tomou Dano");
        Character.enabled = true;
        time = Cooldown;
    }

    void Die()
    {
        if(CurrentHealth <= 0)
        {
            Character.enabled = false;
            Head.GetComponent<Collider>().enabled = true;
            Head.GetComponent<Rigidbody>().isKinematic = false;
            Pmove.enabled = false;
            time = 2f;
        }
    }
}
