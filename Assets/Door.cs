using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public Animator aniDoor;
    [SerializeField] bool Open;
    public void Active()
    {
        if(!Physics.Raycast(transform.position - new Vector3(0,1,0), -transform.forward, 0.5f))
        {
            if(!Open)
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
