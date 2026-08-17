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

    [Header("Risk Settings")]
    [Tooltip("Porcentaje de probabilidad (0 a 100) de perder CADA ítem si se abre a la fuerza.")]
    [SerializeField][Range(0f, 100f)] private float DestroyRiskPercentage = 40f;



    public void Interact(GameObject Interactor)
    {
        if (Interactor == null) return;

        if (Items.Length == 0) return;

        if (NeedKey == false)
        {
            OpenChest(false);
            return;
        }

        if (Interactor.GetComponent<PlayerAttributes>().KeysRemaining() > 0)
        {
            Interactor.GetComponent<PlayerAttributes>().UseKey();

            OpenChest(false);
        }
        else
        {
            Debug.Log("Necesitas llave!!");
        }
    }

    private void OpenChest(bool isForcedOpen)
    {
        for (int i = 0; i < ItemsCount; i++)
        {
            // Si fue forzado, calculamos la probabilidad de destrucción por cada ítem
            if (isForcedOpen)
            {
                float roll = Random.Range(0f, 100f);

                // Si el número aleatorio cae dentro del riesgo, el ítem se destruye
                if (roll <= DestroyRiskPercentage)
                {
                    Debug.Log("¡Un ítem fue destruido por la fuerza del impacto!");
                    continue; // El comando 'continue' salta esta iteración y pasa a la siguiente, evitando el Instantiate
                }
            }

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

            OpenChest(true);
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
            OpenChest(true);
        }
        else
        {
            Debug.Log("Necesitas nivel:" + MeleeDefense.ToString() + " Tu nivel: " + Interactor.GetComponent<Attack>().GetCurrentMeleeLevel());
        }
    }

}
