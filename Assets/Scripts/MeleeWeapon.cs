using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] GameObject Marca;
    [SerializeField] Animator Anim, HandAnim;
    public Inventory Inv;
    [SerializeField] AudioSource Source;
    [SerializeField] AudioClip[] clips;

    public float Cooldown;

    public bool Equipado = false;

    private void Start()
    {
        HandAnim = GameObject.Find("AnimFixed").GetComponent<Animator>();
        Inv = GetComponentInParent<Inventory>();
    }

    private void Update()
    {
        if(Inv != null)
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
            Equipado = GetComponent<infoItem>().Equipado;
            if (Equipado)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && Cooldown <= 0)
                {
                    GetComponent<Collider>().enabled = true;
                    HandAnim.SetInteger("Type", 1);
                    HandAnim.SetTrigger("Click");
                    Cooldown = 1f;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<StalkerIA>() != null)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponentInParent<StalkerIA>().Life -= Random.Range(1, 2);
                other.GetComponentInParent<StalkerIA>().HitProjectile();
                Debug.Log("Corpo");
            }
            else if (other.CompareTag("EnemyHead"))
            {
                other.GetComponentInParent<StalkerIA>().Life -= 5;
                other.GetComponentInParent<StalkerIA>().HitProjectile();
                //other.GetComponentInParent<StalkerIA>().Ragdoll(true);
                Debug.Log("Cabeça");
            }
        }
    }
}
