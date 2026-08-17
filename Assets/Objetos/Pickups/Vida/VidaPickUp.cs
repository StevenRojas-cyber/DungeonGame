using UnityEngine;

public class VidaPickUp : MonoBehaviour, IPickUp
{
    [Header("PickUp Stats")]
    [SerializeField] private float HealCapacity = 10f;

    public void Effect(GameObject User)
    {
        User.GetComponent<PlayerAttributes>().Heal(HealCapacity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Effect(collision.gameObject);
        }

        Destroy(gameObject);
    }
}
