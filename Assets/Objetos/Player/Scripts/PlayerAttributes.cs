using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour, IDamageable
{
    [Header("Player Components")]
    [SerializeField] private Collider2D PlayerCollider;
    [SerializeField] private PlayerControl PlayerControlScript;
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private TMP_Text KeysText;
    [SerializeField] private TMP_Text MagicLevelText;
    [SerializeField] private Image HealthBar;


    [Header("Player Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private int Keys;
    [SerializeField] private int MagicLevel;

    private float currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
        HealthText.text = currentHealth.ToString();
        KeysText.text = Keys.ToString();
    }

    void Update()
    {
       UpdateStatsUI();

       
    }

    public void GetKey()
    {
        Keys++;
    }

    public void UseKey()
    {
        if (Keys > 0)
        {
            Keys--;
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

    void UpdateStatsUI()
    {
        HealthText.text = currentHealth.ToString();
        KeysText.text = Keys.ToString();
        MagicLevelText.text = MagicLevel.ToString();

        HealthBar.fillAmount = currentHealth / maxHealth;
    }

    public float GetMagicLevel()
    {
        return MagicLevel;
    }
}
