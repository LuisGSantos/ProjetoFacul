using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItens : MonoBehaviour
{
    [SerializeField] GameObject[] _Drop;

    public void Drop()
    {
        if(_Drop.Length > 1)
        {
            for(int i = 0; i <= _Drop.Length; i++)
            {
                Instantiate(_Drop[i],transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
            }
        }
        else
        {
            Instantiate(_Drop[0], transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }
}
