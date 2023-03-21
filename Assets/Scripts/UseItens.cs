using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItens : MonoBehaviour
{
    [SerializeField] Inventory Inv;
    public Animator HandAnim;

    public float cooldown;
    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        if (Inv.CurrentWeapon != null)
        {
            HandAnim.SetInteger("Type", Type(Inv.CurrentWeapon.GetComponent<infoItem>().Type));
        }
        else
            HandAnim.SetInteger("Type", Type(""));
        HitMeleeWeapon();
    }
    int Type(string type)
    {
        if (type != "Melee" && type != "Projectile" || type == null)
        {
            return 0;
        }
        else if (type == "Melee")
        {
            return 1;
        }
        else if (type == "Projectile")
        {
            return 2;
        }
        else if (type == "MedicKit")
        {
            return 3;
        }
        else return 0;
    }


    void HitMeleeWeapon()
    {
        if (Inv.CurrentWeapon != null && Inv.CurrentWeapon.GetComponent<infoItem>().Type == "Melee")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown <= 0)
            {
                HandAnim.SetTrigger("Click");
                Inv.CurrentWeapon.GetComponent<Collider>().enabled = true;
                cooldown = 1f;
            }
        }
    }


    void EventAnimCooldown()
    {
        Inv.CurrentWeapon.GetComponent<Collider>().enabled = false;
    }
}
