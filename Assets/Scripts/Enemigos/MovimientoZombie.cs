using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoZombie : MonoBehaviour
{
    [Header("Mode")]
    [SerializeField] private bool walk;

    [Header("Logic")]
    [SerializeField] public float velocidad;
    [SerializeField] public float velocidadDefault;
    [SerializeField] private bool isIdling;
    [SerializeField] private Transform controladorPared;
    [SerializeField] private Transform controladorOjos;

    [SerializeField] private Transform controladorIdleR;
    [SerializeField] private Transform controladorIdleL;
    [SerializeField] public LayerMask groundLayer;

    [SerializeField] private float distanciaPared;
    [SerializeField] private bool moviendoDerecha;
    [SerializeField] private bool isWalking = false;
    private Animator animator;
    private Rigidbody2D rb;

    public LayerMask capaPared;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocidad = velocidadDefault;
    }

    private void FixedUpdate()
    {
        RaycastHit2D informacionPared = Physics2D.Raycast(controladorPared.position, moviendoDerecha ? Vector2.right : Vector2.left, distanciaPared, capaPared);
        RaycastHit2D informacionOjos = Physics2D.Raycast(controladorOjos.position, moviendoDerecha ? Vector2.right : Vector2.left, distanciaPared, capaPared);
        if (walk)
        {

            if (!isIdling)
            {
                rb.velocity = new Vector2(velocidad, rb.velocity.y);
            }

            if (informacionPared || informacionOjos)
            {
                Girar();
            }


            if (Physics2D.OverlapBox(controladorIdleR.position, controladorIdleR.localScale, 0, groundLayer) && Physics2D.OverlapBox(controladorIdleL.position, controladorIdleL.localScale, 0, groundLayer))
            {
                velocidad = 0;
                Debug.Log("Zombie se choca por ambos lados");
                isIdling = true;
            }
            else
            {
                isIdling = false;
                //velocidad = velocidadDefault;
            }


            if (velocidad != 0)
            {
                isWalking = true;
            }
            else if (velocidad == 0)
            {
                isWalking = false;
            }
            animator.SetBool("isWalking", isWalking);
        }
        else
        {
            velocidad = 0;
            velocidadDefault = 0;
        }
    }

    private void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
        velocidadDefault *= -1;
    }

    public void SetSpeed(float newSpeed)
    {
        velocidad = newSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(controladorPared.transform.position, controladorPared.transform.position + (moviendoDerecha ? Vector3.right : Vector3.left) * distanciaPared);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(controladorOjos.transform.position, controladorOjos.transform.position + (moviendoDerecha ? Vector3.right : Vector3.left) * distanciaPared);

    }
}
