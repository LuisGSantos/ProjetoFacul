using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helio : MonoBehaviour
{

    Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = transform.up * Time.deltaTime * 10f;
    }
}
