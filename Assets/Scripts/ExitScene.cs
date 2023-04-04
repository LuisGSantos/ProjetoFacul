using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1.5f);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Exit"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Descendo");
                    Application.Quit();
                }
            }
        }
    }
}
