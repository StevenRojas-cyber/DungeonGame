using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour, IDamageable, IKillable
{
    [Header("Player Components")]
    [SerializeField] private Collider2D PlayerCollider;
    [SerializeField] private PlayerControl PlayerControlScript;
    [SerializeField] private GameObject AttackArea;

    [Header("Player Attributes")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    //[SerializeField] private int Keys = 0;



    void Start()
    {

        
    }

    
    void Update()
    {
        
    }

    public void Kill()
    {
        Debug.Log("Player has been killed.");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Kill();
        }
    }
}
