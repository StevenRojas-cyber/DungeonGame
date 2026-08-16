using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour, IDamageable
{
    [Header("Player Components")]
    [SerializeField] private Collider2D PlayerCollider;
    [SerializeField] private PlayerControl PlayerControlScript;
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private TMP_Text KeysText;

    [Header("Player Attributes")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    [SerializeField] private int Keys = 0;

    public void GetKey()
    {
        Keys++;
        KeysText.text = "Keys: " + Keys;
        Debug.Log("Player has obtained a key. Total keys: " + Keys);
    }

    public void UseKey()
    {
        if (Keys > 0)
        {
            Keys--;
            KeysText.text = "Keys: " + Keys;
        }
    }

    public int KeysRemaining()
    {
        return Keys;
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
