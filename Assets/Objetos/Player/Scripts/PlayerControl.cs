using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("Speed Movement")]
    [SerializeField] private float speed = 5f;

    [Header("Components")]
    [SerializeField] private GameObject AttackArea;

    private Vector2 moveDirection;
    private Vector2 lookDirection;
    private Rigidbody2D PlayerBody2D;
    private Transform characterTransform;
    private Transform attackAreaTransform;


    void Start()
    {
        PlayerBody2D = GetComponent<Rigidbody2D>();
        characterTransform = GetComponent<Transform>();
        attackAreaTransform = AttackArea.GetComponent<Transform>();
    }

    
    void Update()
    {
        if (PlayerBody2D == null || characterTransform == null) return;

        Vector3 movement = new Vector3(moveDirection.x * speed, moveDirection.y * speed, 0);
        PlayerBody2D.linearVelocity = movement;
    }

    public void Move(InputAction.CallbackContext context)
    {
       
        moveDirection = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {

        Vector2 mouseScreenPosition = context.ReadValue<Vector2>();

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    
        Vector3 direction = mouseWorldPosition - characterTransform.position;

        float angleOrientation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        attackAreaTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angleOrientation - 90f));
        
        float angleSnap = Mathf.Round(angleOrientation / 90f) * 90f;

        characterTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angleSnap - 90f));

    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
          SpriteRenderer areaColor = AttackArea.GetComponent<SpriteRenderer>();
            areaColor.color = Color.red;

        }
        else
        {
            SpriteRenderer areaColor = AttackArea.GetComponent<SpriteRenderer>();
            areaColor.color = Color.white;
        }
    }
}
