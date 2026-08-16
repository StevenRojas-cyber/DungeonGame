using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour, IDamageable
{

    [Header("Enemy Components")]
    [SerializeField] private float MaxHealth;
    
    private float currentHealth;

    void Start()
    {
        currentHealth = MaxHealth;
    }

    
    void Update()
    {
        
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
