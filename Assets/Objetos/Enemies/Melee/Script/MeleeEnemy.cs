using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour, IDamageable
{

    [Header("Enemy Stats")]
    [SerializeField] private float MaxHealth;
    [SerializeField] private float Damage;
    [SerializeField] private float Speed;


    [Header("Enemy Components")]
    [SerializeField] private Rigidbody2D Enemybody2D;
    [SerializeField] private Animator EnemyAnimator;
    [SerializeField] private Collider2D AttackHitbox;
    
    private bool CanAttack = false;
    private float currentHealth;
    private Vector2 moveDirection;

    private Transform Target;
    private Transform AttackZone;

    void Start()
    {
        currentHealth = MaxHealth;
        AttackHitbox.enabled = false;

        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        if (Target == null) return;

        moveDirection = (Target.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * Speed * Time.deltaTime;

        CalculateDistance();
        ManageAnimations();
        LookAtPlayer();
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the attack area.");

            other.GetComponent<IDamageable>().TakeDamage(Damage);
        }
    }

    void ManageAnimations()
    {
        if (EnemyAnimator == null) return;

        EnemyAnimator.SetBool("CanAttack", CanAttack);
        EnemyAnimator.SetFloat("MoveMagnitude", moveDirection.magnitude);
    }

    void CalculateDistance()
    {
        float distance = (Target.position - transform.position).magnitude;
        
        if(distance < 2.5f)
        {
            CanAttack = true;
        }
        else
        {
            CanAttack = false;
        }

    }

    void LookAtPlayer()
    {
        Vector2 direction = Target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x);

        float LookX = Mathf.Cos(angle);
        float LookY = Mathf.Sin(angle);

        EnemyAnimator.SetFloat("LookX", LookX);
        EnemyAnimator.SetFloat("LookY", LookY);
    }

    public void ActiveHitBox()
    {
        if (EnemyAnimator == null) return;

        AttackHitbox.offset = Target.position;
        AttackHitbox.enabled = true;
    }

    public void DeactiveHitBox()
    {
        if (EnemyAnimator == null) return;

        AttackHitbox.offset = Target.position;
        AttackHitbox.enabled = false;

    }
}
