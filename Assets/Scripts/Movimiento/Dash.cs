using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    private Rigidbody2D rb;
    private MovimientoPersonaje personaje;
    private float gravity;

    public float tiempoDash = 0.2f;
    public float fuerzaDash = 20f;
    public float tiempoEspera = 1f;

    private bool dasheando;
    private bool puedeDashear = true;
    public bool Dasheando => dasheando;
    private Vector2 dashingDir;

    // Start is called before the first frame update
    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
       personaje = GetComponent<MovimientoPersonaje>();
       gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(Dasher());

        }
    }

    private IEnumerator Dasher()
    {
        if ((personaje.dirHorizontal != 0 || personaje.dirVertical != 0) && puedeDashear)
        {
            dasheando = true;
            dashingDir = new Vector2 (personaje.dirHorizontal, personaje.dirVertical);
            puedeDashear = false;
            rb.gravityScale = 0f;
            rb.velocity = dashingDir.normalized * fuerzaDash;
            yield return new WaitForSeconds(tiempoDash);
            dasheando = false;
            rb.gravityScale = gravity;
            yield return new WaitForSeconds(tiempoEspera);
            puedeDashear = true;
        }
        
    }

}
