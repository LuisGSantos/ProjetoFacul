using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public string TagInteract;
    RaycastHit hit;
    public Inventory inv;

    private void Start()
    {
    }
    void Update()
    {
        if (Physics.Raycast(transform.position,transform.forward, out hit, 2f))
        {
            if (Vector3.Distance(transform.position, hit.point) <= 2 && hit.transform.CompareTag(TagInteract))
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if(hit.collider.gameObject.GetComponent<Door>().Locked)
                    {
                        hit.collider.gameObject.GetComponent<Door>().Key(inv.KeyInv.Contains(hit.collider.gameObject.GetComponent<Door>().thisKey));
                        Debug.Log(inv.KeyInv.Contains(hit.collider.gameObject.GetComponent<Door>().thisKey));
                    }
                    hit.collider.gameObject.GetComponent<Door>().Active();
                }
            }
        }
    }
}
