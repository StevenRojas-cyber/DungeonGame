using UnityEngine;

public class WoodDoor : MonoBehaviour, IInteractable, IDestructible
{
    [Header("Door Attributes")]
    [SerializeField] private int MagicDefense = 3;

    void Start()
    {
        
    }

    // Update is called once per frame
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

        if(Interactor.GetComponent<FireBall>().GetMagicDamage() >= MagicDefense)
        {
            
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Te falta nivel " + Interactor.GetComponent<FireBall>().GetMagicDamage());
        }
    }
}
