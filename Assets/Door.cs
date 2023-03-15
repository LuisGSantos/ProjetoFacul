using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator aniDoor;
    public void Active()
    {
        if(!Physics.Raycast(transform.position - new Vector3(0,1,0), -transform.forward, 0.5f))
            aniDoor.SetTrigger("Open");
    }
}
