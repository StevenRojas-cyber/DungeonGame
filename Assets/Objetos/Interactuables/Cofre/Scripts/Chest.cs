using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable, IDestructible
{
    [Header("Chest Defense")]
    [SerializeField] private float MeleeDefense = 1;
    [SerializeField] private float MagicDefense = 1;


    [Header("Dropplable Objects")]
    [SerializeField] private bool NeedKey = false;
    [SerializeField] private float ItemsCount = 1;
    [SerializeField] private GameObject[] Items;


    [Header("Drop Settings")]
    [SerializeField] private float scatterRange = 0.5f;



    public void Interact(GameObject Interactor)
    {
        if (Interactor == null) return;

        if (Items.Length == 0) return;

        if (NeedKey == false)
        {
            OpenChest();
            return;
        }

        if (Interactor.GetComponent<PlayerAttributes>().KeysRemaining() > 0)
        {
            Interactor.GetComponent<PlayerAttributes>().UseKey();

            OpenChest();
        }
        else
        {
            Debug.Log("Necesitas llave!!");
        }
    }

    private void OpenChest()
    {
        for (int i = 0; i < ItemsCount; i++)
        {
            GameObject ItemToSpawn = GetRandomItem();

            Vector3 randomOffset = new Vector3(
                Random.Range(-scatterRange, scatterRange),
                Random.Range(-scatterRange, scatterRange),
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

    public void MagicDestroy(GameObject Interactor)
    {
        if (Interactor.gameObject.tag != "FireBall") return;

        if (Interactor.GetComponent<FireBall>().GetCurrentMagicLevel() >= MagicDefense)
        {

            OpenChest();
        }
        else
        {
            Debug.Log("Necesitas nivel: " + MagicDefense);
        }
    }

    public void MeleeDestroy(GameObject Interactor)
    {
        if (Interactor.gameObject.tag != "AttackArea") return;

        if (Interactor.GetComponent<Attack>().GetCurrentMeleeLevel() >= MeleeDefense)
        {
            OpenChest();
        }
        else
        {
            Debug.Log("Necesitas nivel:" + MeleeDefense.ToString() + " Tu nivel: " + Interactor.GetComponent<Attack>().GetCurrentMeleeLevel());
        }
    }

}
