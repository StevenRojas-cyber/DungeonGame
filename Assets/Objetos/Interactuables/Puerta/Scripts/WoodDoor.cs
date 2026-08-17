using UnityEngine;

public class WoodDoor : MonoBehaviour, IInteractable, IDestructible,IDamageable
{
    [Header("Door Attributes")]
    [SerializeField] private int MagicDefense = 3;
    [SerializeField] private float MeleeDefense;

    
    void Update()
    {
        
    }

    public void Interact(GameObject Interactor)
    {
        if (Interactor.gameObject.tag != "Player") return;

        if(Interactor.GetComponent<PlayerAttributes>().KeysRemaining() > 0)
        {
            Interactor.GetComponent<PlayerAttributes>().UseKey();
            OpenDoor();
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OpenDoor()
    {
        // Logic to open the door
        Destroy(this.gameObject);
    }

    public void MagicDestroy(GameObject Interactor)
    {
        if(Interactor.gameObject.tag != "FireBall") return;

        if(Interactor.GetComponent<FireBall>().GetCurrentMagicLevel() >= MagicDefense)
        {
            
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Te falta nivel " + Interactor.GetComponent<FireBall>().GetCurrentMagicLevel());
        }
    }

    public void MeleeDestroy(GameObject Interactor)
    {
        if(Interactor.gameObject.tag != "AttackArea") return;
        
        if(Interactor.GetComponent<Attack>().GetCurrentMeleeLevel() >= MeleeDefense)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Te falta nivel " + Interactor.GetComponent<Attack>().GetCurrentMeleeLevel());
        }
    }


    public void TakeDamage(float damage)
    {
        MeleeDefense -= damage;
        if (MeleeDefense <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Debug.Log("Door destroyed");
        Destroy(this.gameObject);
    }

}
