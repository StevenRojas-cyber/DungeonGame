using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Prjectile Speed")]
    [SerializeField] private float speed = 5f;

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
       string tag = collision.gameObject.tag;

        switch (tag)
        {
            case "WoodDoor":
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                break;

            case "Wall":
                Destroy(this.gameObject);
                break;

        }
    }

}
