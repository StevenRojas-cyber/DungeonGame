using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour, IDamageable
{

    [Header("Enemy Components")]
    [SerializeField] private float MaxHealth;
    [SerializeField] private float Damage;
    [SerializeField] private float Speed;

    private Rigidbody2D Enemybody2D;
    private Transform Target;
    private Vector2 moveDirection;

    private float currentHealth;


    void Awake()
    {
        Enemybody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = MaxHealth;

        Target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    
    void Update()
    {
        moveDirection = (Target.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * Speed * Time.deltaTime;
    }




    public void Kill()
    {
        Debug.Log("Enemy has been killed.");
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy took " + damage + " damage.");

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

}
