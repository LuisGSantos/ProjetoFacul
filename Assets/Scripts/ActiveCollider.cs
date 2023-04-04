using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCollider : MonoBehaviour
{
    public Inventory Inv;

    private void Start()
    {
       Inv = GetComponentInParent<Inventory>();
    }
    void Collider()
    {
        Inv.CurrentWeapon.GetComponent<Collider>().enabled = false;
    }
}
