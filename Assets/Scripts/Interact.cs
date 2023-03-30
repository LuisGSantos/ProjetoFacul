using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public string TagInteract;
    RaycastHit hit;
    public Inventory inv;

    void Update()
    {
        if (Physics.Raycast(transform.position,transform.forward, out hit, 2f))
        {
            if (Vector3.Distance(transform.position, hit.point) <= 2 && hit.transform.CompareTag("Door"))
            {
                if(hit.collider.gameObject.GetComponent<Door>())
                {
                    if (inv.KeyInv.Contains(hit.collider.gameObject.GetComponent<Door>().thisKey) && hit.collider.gameObject.GetComponent<Door>().Locked == true)
                    {
                        hit.collider.gameObject.GetComponent<Door>().InfoDoor.Description = "[E] - Destrancar porta";
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (hit.collider.gameObject.GetComponent<Door>().Locked == true)
                        {
                            hit.collider.gameObject.GetComponent<Door>().Key(inv.KeyInv.Contains(hit.collider.gameObject.GetComponent<Door>().thisKey));
                            Debug.Log(inv.KeyInv.Contains(hit.collider.gameObject.GetComponent<Door>().thisKey));
                        }
                        else if (hit.collider.gameObject.GetComponent<Door>().Locked == false)
                            hit.collider.gameObject.GetComponent<Door>().Active();
                    }
                }
            }
        }
    }
}
