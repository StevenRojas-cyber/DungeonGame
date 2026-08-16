using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public void Interact(GameObject Interactor)
    {
        if (Interactor == null) return;

        Interactor.GetComponent<PlayerAttributes>()?.GetKey();

        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
