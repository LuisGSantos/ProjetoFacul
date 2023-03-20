using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int[] FastInv;
    public List<int> KeyInv = new List<int>();
    [SerializeField] Push Pscript;
    [SerializeField] ItemList Itens;

    public int CurrentSlot;
    public GameObject CurrentWeapon;
    public Transform Hand,Head;

    RaycastHit hit;

    private void Start()
    {
        Itens = GameObject.Find("Canvas").GetComponent<ItemList>();
    }

    void Update()
    {
        
        int TempSlot = Input.GetKeyDown(KeyCode.X) ? 0 : Input.GetKeyDown(KeyCode.Alpha1) ? 1 : Input.GetKeyDown(KeyCode.Alpha2) ? 2 : Input.GetKeyDown(KeyCode.Alpha3) ? 3 : CurrentSlot;
        if(CurrentSlot != TempSlot)
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                CurrentSlot = TempSlot;
                SwitchSlot(CurrentSlot);
            }
        }
          
        if(CurrentSlot > 0)
        {
            Pscript.enabled = false;
        }
        else
            Pscript.enabled = true;

        Physics.Raycast(Head.position, Head.forward, out hit, 2f);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Item"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 1; i < FastInv.Length;)
                    {
                        if (FastInv[i] == 0)
                        {
                            FastInv[i] = hit.collider.gameObject.GetComponent<infoItem>().ID;
                            Destroy(hit.collider.gameObject);
                            CurrentSlot = i;
                            SwitchSlot(CurrentSlot);
                            break;
                        }
                        else
                            i++;
                    }
                }
            }
            if (hit.collider.gameObject.CompareTag("Key"))
            {
                int temp = hit.collider.gameObject.GetComponent<infoItem>().ID;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!KeyInv.Contains(temp))
                    {
                        KeyInv.Add(temp);
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    void SwitchSlot(int Slot)
    {
        Destroy(CurrentWeapon);
        CurrentWeapon = Instantiate(Itens.AllItensList[FastInv[Slot]], Hand);
        CurrentWeapon.tag = CurrentWeapon.GetComponent<infoItem>().Type;
        CurrentWeapon.GetComponent<Rigidbody>().isKinematic = true;
        CurrentWeapon.layer = 2;
    }
}
