using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    public Animator animator; //Importacion de animador
    public Rigidbody2D rb;
    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;
    

    [Header("Jumping")]
    public float jumpPower = 20f;
    public int maxHover = 1;
    private int remainingHover;
    public float hoverDuration = 1f;
    private float currHoverTime = 0;
    private bool canHover = false;
    private bool pressingJump = false;
    private float pressingJumpTime = 0;

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



    void Start()
    {
        defaultFallSpeed = maxFallSpeed;
    }

    void Update()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        Gravity();
        GroundCheck();

        if (pressingJump)
        {
            if (!isGrounded)
            {
               
                if(remainingHover < maxHover)
                {
                    Hover(pressingJumpTime);
                }
            }
            pressingJumpTime += Time.deltaTime;
        }
    }

    private void Gravity()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier; //se cae mas rapido
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    public void Move(InputAction.CallbackContext context) 
    { 
        horizontalMovement = context.ReadValue<Vector2>().x;
        if (horizontalMovement > 0) //Left
        {
            transform.localScale = new Vector3(1f, 1f);
        }
        if (horizontalMovement < 0) //Right
        {
            transform.localScale = new Vector3(-1f, 1f);
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement)); //Movimiento
        animator.SetFloat("SpeedY", rb.velocity.y);
    }


    private void Hover(float pressTime)
    {
        Debug.Log(canHover);
        if (pressingJumpTime == 0)
        {
            currHoverTime = 0;
        }
        if(pressingJumpTime > 0 && canHover)
        {
            currHoverTime += Time.deltaTime;
            Debug.Log("Hovering " + currHoverTime + "/" + hoverDuration);

            if (currHoverTime < hoverDuration)
            {
                maxFallSpeed = hoverFallSpeed;
            }
            else
            {
                maxFallSpeed = defaultFallSpeed;
                Debug.Log("holap");
                canHover = false;
            }
        }
        if (pressingJumpTime < 0)
        {
            Debug.Log("Adios");
            maxFallSpeed = defaultFallSpeed;
            canHover = false;
        }
    }
       

    public void Jump(InputAction.CallbackContext context) 
    {
        
        if (context.performed)
        {
            pressingJump = true;
            pressingJumpTime = 0;
            animator.SetBool("IsJumping", true); //Salto
        }
        else if(context.canceled)
        {
            pressingJump = false;
            pressingJumpTime = -1;
            Hover(pressingJumpTime);
        }

        if (remainingHover > 0)
        {
            if (context.performed) { 

               
                //Hold para salto completo
                if (remainingHover == maxHover)
                {
                    
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                }
                else
                {
                    //Hover :33
                    canHover = true;
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower/2);
                    

                }
                remainingHover--;
            }
            else if (context.canceled)
            {
                //Light tap para salto a medias
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                remainingHover--;
            }
        }
       
    }

    private void GroundCheck()
    {
        isGrounded = false;
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            remainingHover = maxHover;
            canHover = true;
            isGrounded = true;
            animator.SetBool("IsJumping", false); //Finalizar Salto
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
