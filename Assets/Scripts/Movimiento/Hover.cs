using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    private Rigidbody2D rb;
    private MovimientoPersonaje personaje;
    private Dash Dash;
    private float gravity;

    public float tiempoHover = 0.05f;
    public float fuerzaHover = 3f;
    public float tiempoEspera = 1f;

    private bool canHover = true;
    private bool suelo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        personaje = GetComponent<MovimientoPersonaje>();
        Dash = GetComponent<Dash>();
        gravity = rb.gravityScale;
    }

    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector3.down, 1.3f))
        {
            suelo = true;
        }
        else suelo = false;
        if (Input.GetKeyDown(KeyCode.UpArrow) && suelo == false && !Dash.Dasheando && canHover)
        {
             StartCoroutine(HoverFunc());

        }
    }

    private IEnumerator HoverFunc()
    {
        canHover = false;

        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x, fuerzaHover);

        yield return new WaitForSeconds(tiempoHover);

        rb.gravityScale = gravity;

        yield return new WaitForSeconds(tiempoEspera);
        canHover = true;
    }
}
