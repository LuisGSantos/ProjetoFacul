using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public string TagInteract;
    RaycastHit hit;

    private void Start()
    {
        
    }
    void Update()
    {
        if(Physics.Raycast(transform.position,transform.forward, out hit, 2f))
        {
            if (Vector3.Distance(transform.position, hit.point) <= 2 && hit.transform.CompareTag(TagInteract))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                 hit.collider.GetComponent<Door>().Active();
            }
        }
    }
}
