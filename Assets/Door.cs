using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator aniDoor;
    public void Active()
    {
        aniDoor.SetTrigger("Open");
    }
}
