using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Culling : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Player.transform.position,transform.position) >= 10)
        {
        
        }
    }
}
