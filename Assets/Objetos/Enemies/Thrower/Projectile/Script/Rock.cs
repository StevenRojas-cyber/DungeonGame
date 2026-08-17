using UnityEngine;

public class Rock : MonoBehaviour
{
    [Header("Projectile Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float BaseDamage;

    private Rigidbody2D BodyProjectile;
    private Collider2D ProjectileHitBox;

    void Start()
    {
        BodyProjectile = GetComponent<Rigidbody2D>();
        ProjectileHitBox = GetComponent<Collider2D>();
    }


    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision == null) return;

        if (collision.gameObject.tag == "Enemigo2") return;

        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(BaseDamage);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
