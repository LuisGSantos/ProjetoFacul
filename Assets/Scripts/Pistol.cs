using UnityEngine;
using UnityEngine.XR;

public class Pistol : MonoBehaviour
{
    [SerializeField] Transform ponta;
    [SerializeField] GameObject Marca;
    [SerializeField] Animator Anim, HandAnim;
    public Inventory Inv;
    [SerializeField] AudioSource Source;
    [SerializeField] AudioClip[] clips;
    public int Ammo, MaxAmmo;

    public float Cooldown;

    public bool Equipado = false;

    private void Start()
    {
        HandAnim = GameObject.Find("AnimFixed").GetComponent<Animator>();
        ponta = Camera.main.GetComponent<Transform>();
        Anim = GetComponent<Animator>();
        Inv = GetComponentInParent<Inventory>();
        if (Inv != null)
        {
            Ammo = Inv.CurrentAmmo;
        }
    }

    private void Update()
    {
        if (Inv != null)
        {
            Equipado = GetComponent<infoItem>().Equipado;
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
            if (Ammo == 0 && Inv != null)
            {
                Anim.SetBool("NoAmmo", true);
            }
            else
                Anim.SetBool("NoAmmo", false);

            if (Inv.Ammo > 0)
            {
                if (Input.GetKeyDown(KeyCode.R) && Cooldown <= 0)
                {
                    if (Inv.Ammo >= 1 && Inv.Ammo <= 7)
                    {
                        Ammo = Inv.Ammo;
                        Inv.Ammo -= Ammo;
                    }
                    else if (Ammo >= 1 && Ammo <= 7)
                    {
                        Inv.Ammo -= Ammo - MaxAmmo;
                        Ammo = MaxAmmo;
                    }
                    else
                    {
                        Ammo = MaxAmmo;
                        Inv.Ammo -= MaxAmmo;
                    }
                    Anim.SetTrigger("Reload");
                    Cooldown = 2f;
                }
            }
            Inv.CurrentAmmo = Ammo;

            if (Equipado)
            {
                if (Ammo > 0)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) && Cooldown <= 0)
                    {
                        Anim.SetTrigger("Click");
                        Cooldown = 0.5f;
                    }
                }
                if (Input.GetKeyDown(KeyCode.V) && Cooldown <= 0)
                {
                    GetComponent<Collider>().enabled = true;
                    HandAnim.SetInteger("Type", 1);
                    HandAnim.SetTrigger("Click");
                    Cooldown = 1f;
                }
            }
        }
    }
    void Projectile()
    {
        RaycastHit hit;
        if (Ammo >= 1)
        {
            Source.volume = 0.8f;
            Source.PlayOneShot(clips[0]);
            Physics.Raycast(ponta.position, ponta.forward, out hit, 100);
            Debug.DrawRay(ponta.position, ponta.forward * 100);
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponentInParent<StalkerIA>().Life -= UnityEngine.Random.Range(5, 14);
                hit.collider.GetComponentInParent<StalkerIA>().HitProjectile();
                Debug.Log("Corpo");
            }
            else if (hit.collider.CompareTag("EnemyHead"))
            {
                hit.collider.GetComponentInParent<StalkerIA>().Life -= Random.Range(15, 25);
                hit.collider.GetComponentInParent<StalkerIA>().HitProjectile();
                Debug.Log("Cabeça");
            }
            else
            {
                GameObject temp;
                temp = Instantiate(Marca, hit.point, Quaternion.LookRotation(Camera.main.transform.up));
                Destroy(temp, 2f);
            }
            Ammo -= 1;
        }
        else
        {
            Anim.SetBool("NoAmmo", true);
        }
    }

    void EventReload()
    {
        if (!Source.isPlaying)
        {
            Source.volume = 0.1f;
            Source.PlayOneShot(clips[1]);
        }
    }
    void EventNoAmmo()
    {
        if (!Source.isPlaying)
        {
            Source.volume = 0.1f;
            Source.PlayOneShot(clips[2]);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<StalkerIA>() != null)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponentInParent<StalkerIA>().Life -= 1;
                other.GetComponentInParent<StalkerIA>().HitProjectile();
                Debug.Log("Corpo");
            }
            else if (other.CompareTag("EnemyHead"))
            {
                other.GetComponentInParent<StalkerIA>().Life -= 3;
                other.GetComponentInParent<StalkerIA>().HitProjectile();
                Debug.Log("Cabeça");
            }
        }
    }

}
