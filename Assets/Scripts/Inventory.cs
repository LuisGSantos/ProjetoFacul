using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int[] FastInv;
    public List<int> KeyInv = new List<int>();
    [SerializeField] Push Pscript;
    public int CurrentSlot;
    [SerializeField] GameObject[] Weapons;
    public GameObject CurrentWeapon;
    public Transform Hand,Head;
    public Animator HandAnim;
    public float cooldown;
    RaycastHit hit;
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        int TempSlot = Input.GetKeyDown(KeyCode.X) ? 0 : Input.GetKeyDown(KeyCode.Alpha1) ? 1 : Input.GetKeyDown(KeyCode.Alpha2) ? 2 : Input.GetKeyDown(KeyCode.Alpha3) ? 3 : CurrentSlot;
        if(CurrentSlot != TempSlot)
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                CurrentSlot = TempSlot;
                SwitchSlot(CurrentSlot);
            }
        }
        HandAnim.SetInteger("IDWeapon", FastInv[CurrentSlot]);
        HitMeleeWeapon();  
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
        if (CurrentWeapon == null)
        {
            CurrentWeapon = Instantiate(Weapons[FastInv[Slot]],Hand);
            CurrentWeapon.tag = CurrentWeapon.GetComponent<infoItem>().Type;
            CurrentWeapon.GetComponent<Rigidbody>().isKinematic = true;
            CurrentWeapon.layer = 2;
        }
    }

    void HitMeleeWeapon()
    {
        if (CurrentWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown <= 0)
            {
                HandAnim.SetTrigger("Click");
                CurrentWeapon.GetComponent<Collider>().enabled = true;
                cooldown = 1f;
            }
        }
    }
        

    void EventAnimCooldown()
    {
        CurrentWeapon.GetComponent<Collider>().enabled = false;
    }
}
