using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementEvan : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float horizontalMovement;
    public float verticalMovement;
    private bool isFacingRight = true;
    private bool isMoving;

    [Header("Jumping")]
    public float jumpPower = 20f;
    public int maxHover = 2;
    private int remainingHover;
    public float hoverDuration = 1f;
    private float currHoverTime = 0f;
    private bool pressingJump = false;
    private float pressingJumpTime = 0f;
    private bool hasJumped = false;
    float groundedDelay = 0.1f;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;
    private bool isGrounded = false;

    [Header("Gravity")]
    public float baseGravity = 5f;
    public float maxFallSpeed = 18f;
    private float defaultFallSpeed;
    public float hoverFallSpeed = 3f;
    public float fallSpeedMultiplier = 1f;

    [Header("Dash")]
    public float dashTime = 0.2f;
    public float dashForce = 20f;
    public float dashWait = 1f;
    private bool isDashing = false;
    private bool canDash = true;
    private float dashRemainingTime = 0f;
    private float dashCooldownTime = 0f;
    private Vector2 dashDirection;


    protected Animator mAnimator;
    public float elemento = 0f;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;


    void Start()
    {
        defaultFallSpeed = maxFallSpeed;
        remainingHover = maxHover;
        mAnimator = GetComponent<Animator>();
    }

    void BasicControls()
    {
        //Actualizar Movimiento
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        //Chekear isGrounded
        GroundCheck();

        //Controlador de gravedad
        Gravity();

        //Controlador del hover
        Hovering();

        //Controlador del dash
        DashCheck();
    }

    private void Update()
    {
        BasicControls();
        mAnimator.SetBool("Horizontal", isMoving);
        mAnimator.SetBool("Grounded", isGrounded);
        mAnimator.SetFloat("Elemento", elemento);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
        verticalMovement = context.ReadValue<Vector2>().y;
        if (horizontalMovement >= 0.1f)
        {
            isGrounded = true;
            isMoving = true;
            transform.localScale = new Vector3(1f, 1f);
            isFacingRight = true;
        }
        else if (horizontalMovement <= -0.1)
        {
            isGrounded = true;
            isMoving = true;
            transform.localScale = new Vector3(-1f, 1f);
            isFacingRight = false;
        }
        else
        {
            isMoving = false;
            isGrounded = true;
        }
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        mAnimator.SetTrigger("Jump");
        if (isGrounded && context.performed)
        {
            //Salto
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            remainingHover--;
            hasJumped = true;
            groundedDelay = 0f;
            ControladorSonido.Instance.EjecutarSonido(jumpSound);
        }
        else if (context.canceled)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Hover(InputAction.CallbackContext context)
    {
        mAnimator.SetTrigger("Jump");
        if (remainingHover > 0 && !isGrounded && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower/2);
            remainingHover--;
            pressingJump = true;
            pressingJumpTime = 0;
        }
        else if (context.canceled)
        {
            pressingJump = false;
            pressingJumpTime = -1;
        }
    }

    private void Hovering()
    {
        //Tiempo de hover
        if (pressingJumpTime == 0)
        {
            currHoverTime = 0;
        }

        //Comprobar el tiempo de salto (si salta)
        if (pressingJump)
        {
            pressingJumpTime += Time.deltaTime;
        }

        if (pressingJumpTime > 0)
        {
            currHoverTime += Time.deltaTime;

            if (currHoverTime < hoverDuration)
            {
                maxFallSpeed = hoverFallSpeed;
            }
            else
            {
                maxFallSpeed = defaultFallSpeed;
            }
        }
        if (pressingJumpTime < 0)
        {
            maxFallSpeed = defaultFallSpeed;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        
        if (context.performed && canDash)
        {
            mAnimator.SetTrigger("Dash");
            ControladorSonido.Instance.EjecutarSonido(dashSound);
            if (horizontalMovement != 0 || verticalMovement != 0)
            {
                Debug.Log("Empiezo Dash");
                canDash = false;
                isDashing = true;
                dashCooldownTime = dashWait;
                dashRemainingTime = dashTime;
            }
        }
    }

    private void PerformDash()
    {
        if (dashRemainingTime > 0)
        {
            Debug.Log("Dasheando");
            isDashing = true;
            dashRemainingTime -= Time.deltaTime;
            if (dashRemainingTime <= 0)
            {
                Debug.Log("Termina Dash");
                dashRemainingTime = 0;
                isDashing = false;
            }
        }
        if (isDashing)
        {
            rb.gravityScale = 0f;
            dashDirection = new Vector2(horizontalMovement, verticalMovement);
            rb.velocity = dashDirection.normalized * dashForce;
        }
        else
        {
            isDashing = false;
            rb.gravityScale = baseGravity;
        }
    }

    private void DashCheck()
    {
        if (isDashing)
        {
            PerformDash();
        }

        if (dashCooldownTime > 0)
        {
            Debug.Log("Cooldown Dash");
            canDash = false;
            dashCooldownTime -= Time.deltaTime;
            if (dashCooldownTime <= 0)
            {
                Debug.Log("Termina Cooldown Dash");
            }
        }
        else
        {
            dashCooldownTime = 0;
            if (isGrounded)
            {
                //Debug.Log("Puedo hacer Dash");
                canDash = true;
            }
        }
    }


    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            if (hasJumped)
            {
                groundedDelay += Time.deltaTime;
                if (groundedDelay >= 0.1f && Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
                {
                    isGrounded = true;
                    remainingHover = maxHover;
                    hasJumped = false;
                }
            }
            else
            {
                isGrounded = true;
                remainingHover = maxHover;
            }
        }
        else
        {
            isGrounded = false;
            if (remainingHover == maxHover)
            {
                remainingHover--;
            }
        }
    }

    private void Gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; //se cae mas rapido
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
