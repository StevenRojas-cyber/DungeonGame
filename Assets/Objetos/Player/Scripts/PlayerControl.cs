using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("Speed Movement")]
    [SerializeField] private float speed = 5f;

    [Header("Components")]
    [SerializeField] private float AttackCooldown;
    [SerializeField] private float SpecialAbilityCooldown;
    [SerializeField] private GameObject AttackArea;
    [SerializeField] private GameObject FireBallPrefab;
    [SerializeField] private Animator Anim;
    [SerializeField] private PlayerAttributes Stats;


    private bool CanInteact = false;
    private bool CanAttack = true;
    private bool ControlEnabled = true;
    private bool CanUseSpecialAbility = true;
    private Vector2 lastMoveDirection;


    private Vector2 moveDirection;
    private Vector2 lookDirection;
    private Rigidbody2D PlayerBody2D;
    private Transform characterTransform;
    private Transform attackAreaTransform;
    private CircleCollider2D InteractArea;
    private GameObject InteractebleObject = null;


    void Start()
    {
        PlayerBody2D = GetComponent<Rigidbody2D>();
        characterTransform = GetComponent<Transform>();
        attackAreaTransform = AttackArea.GetComponent<Transform>();
        InteractArea = GetComponent<CircleCollider2D>();

        AttackArea.GetComponent<Collider2D>().enabled = false;
        
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
        
    }

  
    public void ActivateControls(bool state)
    {
        ControlEnabled = state;
    }



    //Funcion que habilita o deshabilita el control del jugador
    public void SetControlEnabled(bool enabled)
    {
        ControlEnabled = enabled;
    }


    //Funciones de input de movimiento
    public void Move(InputAction.CallbackContext context)
    {
        if(!ControlEnabled) return;

        moveDirection = context.ReadValue<Vector2>();

    }


    //Funcion de mirar
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

    }





    //Funciones de ataque
    public void Attack(InputAction.CallbackContext context)
    {
        if (!ControlEnabled || !CanAttack) return;

        if (context.started)
        {

            StartCoroutine(AttackSequence(AttackCooldown));

        }

    }

    private IEnumerator AttackSequence(float cooldownTime)
    {
        CanAttack = false;

        AttackArea.GetComponent<Collider2D>().enabled = true;
        AttackArea.GetComponent<Animator>().SetBool("IsAttacking", true);

        yield return new WaitForSeconds(0.5f);

        AttackArea.GetComponent<Collider2D>().enabled = false;
        AttackArea.GetComponent<Animator>().SetBool("IsAttacking", false);

        yield return new WaitForSeconds(cooldownTime);

        CanAttack = true;
    }



    //Funciones de ataque especial
    public void MagicAttack(InputAction.CallbackContext context)
    {
       
        if (!ControlEnabled || !CanUseSpecialAbility) return;

        if (context.started)
        {
           Instantiate(FireBallPrefab, attackAreaTransform.position, attackAreaTransform.rotation);
           
        }

        StartCoroutine(SpecialAbilityColdown(SpecialAbilityCooldown));
    }


    private IEnumerator SpecialAbilityColdown(float cooldownTime)
    {
        CanUseSpecialAbility = false;

        yield return new WaitForSeconds(cooldownTime);

        CanUseSpecialAbility = true;
    }

    //Funciones de animacion
    void Animate()
    {
        if(Anim == null) return;

        Anim.SetFloat("MoveMagnitude", moveDirection.magnitude);
    }

    

    //Funcion de interaccion
    public void Interact(InputAction.CallbackContext context)
    {
        
        if (!ControlEnabled || !CanInteact) return;
        if(InteractebleObject == null) return;

        if (context.started)
        {
            InteractebleObject.GetComponent<IInteractable>().Interact(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            CanInteact = true;
            InteractebleObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            CanInteact = false;
            InteractebleObject = null;
        }
    }
}
