using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("Speed Movement")]
    [SerializeField] private float speed = 5f;

    [Header("Components")]
    [SerializeField] private GameObject AttackArea;
    [SerializeField] private GameObject FireBallPrefab;
    [SerializeField] private Animator Anim;

    private bool ControlEnabled = true;
    private Vector2 lastMoveDirection;

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
        
        Animate();

    }

    private void FixedUpdate()
    {
        if (PlayerBody2D == null || characterTransform == null) return;

        Vector3 movement = new Vector3(moveDirection.x * speed, moveDirection.y * speed, 0);
        PlayerBody2D.linearVelocity = movement;
        
        //transform.rotation = Quaternion.identity;
    }


    //Funcion que habilita o deshabilita el control del jugador
    public void SetControlEnabled(bool enabled)
    {
        ControlEnabled = enabled;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(!ControlEnabled) return;

        moveDirection = context.ReadValue<Vector2>();

    }

    public void Look(InputAction.CallbackContext context)
    {
        if (!ControlEnabled) return;

        Vector2 mouseScreenPosition = context.ReadValue<Vector2>();

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    
        Vector3 direction = mouseWorldPosition - characterTransform.position;

        float angleOrientation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        attackAreaTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angleOrientation - 90f));




        //Animacion del personaje segun la direccion del mouse
        float angleSnap = Mathf.Round(angleOrientation / 90f) * 90f;

        float rad = angleSnap * Mathf.Deg2Rad;
        float LookX = Mathf.Round(Mathf.Cos(rad));
        float LookY = Mathf.Round(Mathf.Sin(rad));

        Anim.SetFloat("LookX", LookX);
        Anim.SetFloat("LookY", LookY);

        //characterTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angleSnap - 90f));

    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!ControlEnabled) return;

        if (context.started)
        {
          SpriteRenderer areaColor = AttackArea.GetComponent<SpriteRenderer>();
            areaColor.color = Color.red;

        }
        
        if(context.canceled)
        {
            SpriteRenderer areaColor = AttackArea.GetComponent<SpriteRenderer>();
            areaColor.color = Color.white;
        }
    }

    public void MagicAttack(InputAction.CallbackContext context)
    {
        if (!ControlEnabled) return;

        if (context.started)
        {
           Instantiate(FireBallPrefab, attackAreaTransform.position, attackAreaTransform.rotation);
           
        }

    }

    void Animate()
    {
        if(Anim == null) return;

        Anim.SetFloat("MoveMagnitude", moveDirection.magnitude);
    }

}
