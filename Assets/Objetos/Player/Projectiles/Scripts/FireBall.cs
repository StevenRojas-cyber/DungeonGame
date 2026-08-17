using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Projectile Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private PlayerAttributes PlayerStats;


    private Rigidbody2D BodyProjectile;
    private Collider2D ProjectileHitBox;
    private float FireBallLevel;
    private float BaseDamage;


    void Start()
    {
        BodyProjectile = GetComponent<Rigidbody2D>();
        ProjectileHitBox = GetComponent<Collider2D>();

        BaseDamage = PlayerStats.GetMagicBaseDamage();

    }

    
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        FireBallLevel = PlayerStats.GetMagicLevel();

        UpdateCurrentMagicDamage();
    }

    public void UpdateCurrentMagicDamage()
    {
        BaseDamage = BaseDamage * FireBallLevel;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision == null) return;

       if(collision.gameObject.tag == "Player") return;

       if(collision.gameObject.CompareTag("WoodDoor"))

        {
            collision.gameObject.GetComponent<IDestructible>()?.MagicDestroy(this.gameObject);
            Destroy(this.gameObject);
            return;
        }

        collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(BaseDamage);

        Destroy(this.gameObject);
       
       
    }

    public float GetCurrentMagicLevel()
    {
        return FireBallLevel;
    }
}
