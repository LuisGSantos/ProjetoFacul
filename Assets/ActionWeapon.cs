using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWeapon : MonoBehaviour
{
    [SerializeField] int[] FastInv;
    [SerializeField] Push Pscript;
    int CurrentSlot = 0;
    [SerializeField] GameObject[] Weapons;
    public GameObject CurrentWeapon;
    public Transform Hand;
    public Animator HandAnim;
    public float cooldown;
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        CurrentSlot = Input.GetKeyDown(KeyCode.X) ? 0 : Input.GetKeyDown(KeyCode.Alpha1) ? 1 : Input.GetKeyDown(KeyCode.Alpha2) ? 2 : Input.GetKeyDown(KeyCode.Alpha3) ? 3 : CurrentSlot;
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchSlot(CurrentSlot);
        }
        HandAnim.SetInteger("IDWeapon", FastInv[CurrentSlot]);
        HitMeleeWeapon();  
        if(CurrentSlot > 0)
        {
            Pscript.enabled = false;
        }
        else
            Pscript.enabled = true;
    }

    void SwitchSlot(int Slot)
    {
        Destroy(CurrentWeapon);
        if(CurrentWeapon == null)
        {
            CurrentWeapon = Instantiate(Weapons[FastInv[CurrentSlot]],Hand);          
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
