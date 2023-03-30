using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator aniDoor;
    public infoItem InfoDoor;

    [SerializeField] bool Open;

    public AudioSource Source;
    public AudioClip[] Clip;
    
    public bool Locked;
    public int thisKey;

    private void Update()
    {
        Debug.DrawRay(transform.position - new Vector3(0, 1, 0), -transform.forward * 0.1f);
        if(Locked)
        {
            InfoDoor.Description = "Requer Chave da " + InfoDoor.Name;
            if(thisKey == 0)
            {
                InfoDoor.Description = "Emperrada";
            }
        }
        else if (!Locked)
        {
            InfoDoor.Description = "[E] - Abrir/Fechar";
        }

    }
    public void Active()
    {
        if(!Locked)
        {
            if (!Physics.Raycast(transform.position - new Vector3(0, 1, 0), -transform.forward, 0.1f))
            {
                if (!Open)
                {
                    Open = true;
                    Source.PlayOneShot(Clip[0]);
                }
                else
                {
                    Open = false;
                }
                aniDoor.SetBool("Open", Open);
            }
        }
    }

    public void Key(bool have)
    {
        if(Locked)
        {
            if(have)
            {
                Locked = false;
                Source.PlayOneShot(Clip[2]);
            }
            else
            {
                Source.PlayOneShot(Clip[3]);
            }
        }
    }
}
