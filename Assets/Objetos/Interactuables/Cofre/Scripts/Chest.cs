using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [Header("Dropplable Objects")]
    [SerializeField] private GameObject[] Items;
    [SerializeField] private float ItemsCount = 1;

    [Header("Drop Settings")]
    [SerializeField] private float scatterRange = 0.5f;
    


    public void Interact(GameObject Interactor)
    {
        if (Interactor == null) return;

        if (Items.Length == 0) return;

        for (int i = 0; i < ItemsCount; i++)
        {
            GameObject ItemToSpawn = GetRandomItem();

            // Calculamos una posición ligeramente distinta para cada ítem
            Vector3 randomOffset = new Vector3(
                Random.Range(-scatterRange, scatterRange),
                Random.Range(-scatterRange,scatterRange),
                0f);

            Instantiate(ItemToSpawn, transform.position + randomOffset, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private GameObject GetRandomItem()
    {
        int randomIndex = Random.Range(0, Items.Length);

        return Items[randomIndex];
    }
}
