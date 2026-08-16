using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack Collider")]
    [SerializeField] private float AttackBaseDamage = 10.0f;
    [SerializeField] private Collider2D Atkcollider;
    [SerializeField] private PlayerAttributes playerStats;

    private float AttackLevel = 1.0f;

    private void Update()
    {
        AttackLevelUpdate();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Atkcollider == null) return;
        
        if(collision.CompareTag("WoodDoor"))
        {
            collision.gameObject.GetComponent<IDestructible>()?.MeleeDestroy(this.gameObject);
            return;
        }

        collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(AttackBaseDamage);
    }

    void AttackLevelUpdate()
    {
        AttackLevel = playerStats.GetMeleeLevel();

        AttackBaseDamage = AttackBaseDamage * AttackLevel;
    }

    public float GetCurrentMeleeLevel()
    {
        return AttackLevel;
    }
}
