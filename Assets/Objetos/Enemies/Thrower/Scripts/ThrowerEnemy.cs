using System.Collections;
using UnityEngine;

public class ThrowerEnemy : MonoBehaviour, IDamageable
{
    [Header("Thrower Stats")]
    [SerializeField] private float MaxHealth;
    [SerializeField] private float Damage;
    [SerializeField] private float Speed;
    [SerializeField] private float ThrowRange;
    [SerializeField] private float ThrowColdownValue;

    [Header("Enemy Components")]
    [SerializeField] private Rigidbody2D Enemybody2D;
    [SerializeField] private Animator EnemyAnimator;
    [SerializeField] private GameObject Rock;
    [SerializeField] private GameObject ThrowOffset;


    private bool CanThrow = false;
    private bool DisableThrow = false;
    private float currentHealth;
    private Vector2 moveDirection;
    private Transform Target;


    void Start()
    {
        currentHealth = MaxHealth;

        Target = GameObject.FindWithTag("Player").transform;
    }

    
    void Update()
    {
        if (Target == null) return;

        moveDirection = (Target.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * Speed * Time.deltaTime;

        LookAtPlayer();
        CalculateDistance();
        ThrowRotate();
        ManageAnimations();
    }


    void CalculateDistance()
    {
        if(DisableThrow) return;

        float distance = (Target.position - transform.position).magnitude;

        if (distance <= ThrowRange)
        {
            CanThrow = true;
        }
        else
        {
            CanThrow = false;
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

    void ThrowRotate()
    {
        Vector2 direction = Target.position - transform.position;
        float OrientationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        ThrowOffset.transform.rotation = Quaternion.Euler(new Vector3(0, 0, OrientationAngle - 90f));
    }

    void ManageAnimations()
    {
        EnemyAnimator.SetFloat("MoveMagnitude", moveDirection.magnitude);
        EnemyAnimator.SetBool("CanThrow", CanThrow);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }


    public void ThrowRock()
    {
        if (!CanThrow || DisableThrow) return;

        Instantiate(Rock, ThrowOffset.transform.position, ThrowOffset.transform.rotation);
        StartCoroutine(ThrowColdown());
    }

    public void StartRockColdown()
    {
    }

    private IEnumerator ThrowColdown()
    {
        DisableThrow = true;

        yield return new WaitForSecondsRealtime(ThrowColdownValue);

        DisableThrow = false;

    }
}
