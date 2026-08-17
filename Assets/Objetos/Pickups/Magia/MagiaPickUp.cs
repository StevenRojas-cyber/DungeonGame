using UnityEngine;

public class MagiaPickUp : MonoBehaviour
{
    [Header("PickUp Stats")]
    [SerializeField] private int MagicLevelsUp = 1;

    public void Effect(GameObject User)
    {
        User.GetComponent<PlayerAttributes>().MagicLevelUP(MagicLevelsUp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Effect(collision.gameObject);
        }

        Destroy(gameObject);
    }
}
