using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoPersonaje : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GameObject bala;
    public GameObject balaFuego;
    public GameObject balaHielo;


    private Rigidbody2D rb;
    private Dash Dash;

    private float Horizontal;
    private float Vertical;
    public float dirHorizontal => Horizontal;
    public float dirVertical => Vertical;
    private bool Suelo;


    private bool puedeDisparar = true;
    public float tipo = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Dash = GetComponent<Dash>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");


        if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1f, 1f);
        } else if (Horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1f, 1f);
        }
     
        float distanciaRaycast = 1.2f;
        Vector2 posicionCentro = transform.position;
        Vector2 posicionIzquierda = posicionCentro + Vector2.left * 0.5f;
        Vector2 posicionDerecha = posicionCentro + Vector2.right * 0.5f;
        Suelo = Physics2D.Raycast(posicionCentro, Vector2.down, distanciaRaycast) ||
                Physics2D.Raycast(posicionIzquierda, Vector2.down, distanciaRaycast) ||
                Physics2D.Raycast(posicionDerecha, Vector2.down, distanciaRaycast);

        if (Input.GetKeyDown(KeyCode.W) && Suelo && !Dash.Dasheando)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Suelo && !Dash.Dasheando)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            tipo++;
            if (tipo > 3)
            {
                tipo = 3;
            }
        } else if (Input.GetKeyDown(KeyCode.X))
        {
            tipo--;
            if (tipo < 1)
            {
                tipo = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && puedeDisparar)
        {
            StartCoroutine(Disparar());
        }
    }

    private IEnumerator Disparar()
    {
        puedeDisparar = false;
        if (tipo == 1)
        {
            GameObject temp = Instantiate(bala, transform.position, transform.rotation);
            Destroy(temp, 3);
        }
        else if (tipo == 2)
        {
            GameObject temp = Instantiate(balaFuego, transform.position, transform.rotation);
            Destroy(temp, 3);
        }
        else if (tipo == 3) {
            GameObject temp = Instantiate(balaHielo, transform.position, transform.rotation);
            Destroy(temp, 3);
        }
        yield return new WaitForSeconds(1);
        puedeDisparar = true;
    }

    private void Jump()
    {
        //if (rb.velocity.y <= 0)
        //{
        rb.AddForce(Vector2.up * JumpForce);
        //}
    }

    private void FixedUpdate()
    {
        if (!Dash.Dasheando)
        {
            rb.velocity = new Vector2(Horizontal * Speed, rb.velocity.y);
        }
    }
}
