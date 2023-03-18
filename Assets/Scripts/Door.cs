using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator aniDoor;
    [SerializeField] bool Open;
    public bool Locked;
    public int thisKey;
    public void Active()
    {
        if(!Locked)
        {
            if (!Physics.Raycast(transform.position - new Vector3(0, 1, 0), -transform.forward, 0.5f))
            {
                if (!Open)
                {
                    Open = true;
                    aniDoor.SetBool("Open", Open);
                }
                else
                {
                    Open = false;
                    aniDoor.SetBool("Open", Open);
                }
            }
        }
    }

    public void Key(bool have)
    {
        if (Locked && have)
        {
            Locked = false;
        }
    }
}
