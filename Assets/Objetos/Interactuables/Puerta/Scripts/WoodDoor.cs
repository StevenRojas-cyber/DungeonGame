using UnityEngine;

public class WoodDoor : MonoBehaviour, IInteractable
{
    public void Interact(GameObject Interactor)
    {
        if (Interactor == null) return;

        if(Interactor.GetComponent<PlayerAttributes>().KeysRemaining() > 0)
        {
            Interactor.GetComponent<PlayerAttributes>().UseKey();
            OpenDoor();
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenDoor()
    {
        // Logic to open the door
        Debug.Log("The door is now open.");
        Destroy(gameObject);
    }
}
