using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventDoor : MonoBehaviour
{
    [SerializeField] Door door;

    public void Batendo()
    {
        door.Source.PlayOneShot(door.Clip[1]);
    }
}
