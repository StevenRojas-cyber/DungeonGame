using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack Collider")]
    [SerializeField] private Collider2D Atkcollider;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Atkcollider == null) return;

        collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(10f);
    }
}
