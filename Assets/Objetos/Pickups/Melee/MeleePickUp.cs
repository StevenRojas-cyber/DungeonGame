using UnityEngine;

public class MeleePickUp : MonoBehaviour
{
    [Header("PickUp Stats")]
    [SerializeField] private int MeleeLevelsUp = 1;

    public void Effect(GameObject User)
    {
        User.GetComponent<PlayerAttributes>().MeleeLevelUP(MeleeLevelsUp);
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
