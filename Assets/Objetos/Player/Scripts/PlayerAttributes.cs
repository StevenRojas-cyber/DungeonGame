using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour, IDamageable
{
    [Header("Player Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private int Keys;
    [SerializeField] private int MagicLevel;
    [SerializeField] private int MeleeLevel;
    [SerializeField] private float BaseMeleeDamage;
    [SerializeField] private float BaseMagicDamage;

    [Header("Player Components")]
    [SerializeField] private Collider2D PlayerCollider;
    [SerializeField] private PlayerControl PlayerControlScript;
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private TMP_Text KeysText;
    [SerializeField] private TMP_Text MagicLevelText;
    [SerializeField] private TMP_Text MeleeLevelText;
    [SerializeField] private Image HealthBar;



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

    public void Heal(float HealCount)
    {
        currentHealth += HealCount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

    }


    void UpdateStatsUI()
    {
        HealthText.text = currentHealth.ToString();
        KeysText.text = Keys.ToString();
        MagicLevelText.text = MagicLevel.ToString();
        MeleeLevelText.text = MeleeLevel.ToString();

        HealthBar.fillAmount = currentHealth / maxHealth;
    }



    public float GetMagicLevel()
    {
        return MagicLevel;
    }
    
    public void MagicLevelUP(int LevelUp)
    {
        MagicLevel += LevelUp;
    }

    public float GetMagicBaseDamage()
    {
        return BaseMagicDamage;
    }





    public float GetMeleeLevel()
    {
        return MeleeLevel;
    }
    public void MeleeLevelUP(int LevelUP)
    {
        MeleeLevel += LevelUP;
    }
    public float GetMeleeBaseDamage()
    {
        return BaseMeleeDamage;
    }

}
