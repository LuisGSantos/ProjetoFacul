using UnityEngine;

public class Baloes : MonoBehaviour
{
    public GameObject balao;
    public Material[] materials;
    void Start()
    {
        for(int i = 0; i <= 25; i++)
        {
            GameObject temp = Instantiate(balao,transform);
            temp.GetComponent<Renderer>().material = materials[Random.Range(0,materials.Length)];
            temp.GetComponent<Rigidbody>().velocity = transform.up * Time.deltaTime * 10f;
        }
    }
}
