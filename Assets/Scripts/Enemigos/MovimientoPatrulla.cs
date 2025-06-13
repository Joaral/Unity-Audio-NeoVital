using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoPatrulla : MonoBehaviour
{
    [Header("Mode")]
    [SerializeField] private bool patrol;
    [Header("Animation")]
    [SerializeField] private bool isWalking = false;
    [SerializeField] private bool isIdling;
    public Animator animator;

    [Header("Blocking")]
    [SerializeField] private Transform controladorIdleR;
    [SerializeField] private Transform controladorIdleL;
    [SerializeField] public LayerMask groundLayer;

    [Header("Logic")] 
    [SerializeField] public float velocidad = 4;
    [SerializeField] public float velocidadDefault;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Transform controladorPared;
    [SerializeField] private Transform controladorEnemigo;
    [SerializeField] private float distanciaSuelo;
    [SerializeField] private float distanciaPared;
    [SerializeField] private float distanciaEnemigo;
    [SerializeField] private bool moviendoDerecha;
    public float tiempoDisparo;

    public bool derecha => moviendoDerecha;
    [SerializeField] private LayerMask capaJugador; // Nueva capa para el jugador
    public GameObject balaEnemigo;
    private Rigidbody2D rb;
    private bool puedeDisparar = true;
    public bool puedeDispararGlobal = true;

    public AudioSource AudioS;
    [SerializeField] private AudioClip shotEnemySound;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AudioS = GetComponent<AudioSource>();
        velocidad = velocidadDefault;
    }

    private void FixedUpdate()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distanciaSuelo, LayerMask.GetMask("Ground"));
        RaycastHit2D informacionPared = Physics2D.Raycast(controladorPared.position, moviendoDerecha ? Vector2.right : Vector2.left, distanciaPared, ~LayerMask.GetMask("Bala", "Player"));
        RaycastHit2D informacionEnemigo = Physics2D.Raycast(controladorEnemigo.position, moviendoDerecha ? Vector2.right : Vector2.left, distanciaEnemigo, capaJugador);


        if (patrol)
        {
        if (informacionEnemigo.collider != null && puedeDisparar && puedeDispararGlobal) // Si el rayo golpea al jugador
        {
            velocidad = 0;
            StartCoroutine(Disparar());
        }
        else if (informacionEnemigo.collider == null && velocidad == 0) 
        {
            velocidad = velocidadDefault;
        }
        if (!isIdling)
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
        }

        if (informacionSuelo == false || informacionPared)
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

        if (velocidad == 0)
        {
            isWalking = false;
        }
        else if (velocidad != 0)
        {
            isWalking = true;
        }
        animator.SetBool("isWalking", isWalking);
        }
        else
        {
            velocidad = 0;
            velocidadDefault = 0;
            if (informacionEnemigo.collider != null && puedeDisparar && puedeDispararGlobal) // Si el rayo golpea al jugador
            {
                StartCoroutine(Disparar());
            }
        }
    }
    private IEnumerator Disparar()
    {
        puedeDisparar = false;
        yield return new WaitForSeconds(tiempoDisparo/2);
        if (AudioS != null)
        {
            AudioS.PlayOneShot(shotEnemySound);
        }
        Vector3 offset = new Vector3((transform.eulerAngles.y > 0 ? -1: 1) * 1.5f, 0, 0);
        if (puedeDispararGlobal)
        {
            GameObject temp = Instantiate(balaEnemigo, transform.position + offset, transform.rotation);
            Destroy(temp, 3);
        }
        yield return new WaitForSeconds(tiempoDisparo);
        puedeDisparar = true;
    }
    private void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 180, 0);
        velocidad *= -1;
        velocidadDefault *= -1;
    }

    public void SetSpeed(float newSpeed)
    {
        velocidad = newSpeed;
    }

    public void SetPuedeDisparar(bool newInfo)
    {
        puedeDispararGlobal = newInfo;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distanciaSuelo);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(controladorPared.transform.position, controladorPared.transform.position + (moviendoDerecha ? Vector3.right : Vector3.left) * distanciaPared);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(controladorEnemigo.transform.position, controladorEnemigo.transform.position + (moviendoDerecha ? Vector3.right : Vector3.left) * distanciaEnemigo);
    }
}
