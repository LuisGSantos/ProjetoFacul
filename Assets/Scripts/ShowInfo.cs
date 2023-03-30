using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] Text Info;
    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2f);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<infoItem>() != null)
            {
                Info.enabled = true;
                Info.text = hit.collider.gameObject.GetComponent<infoItem>().Description;
            }
            else
                Info.enabled = false;
        }
        else
            Info.enabled = false;
    }
}
