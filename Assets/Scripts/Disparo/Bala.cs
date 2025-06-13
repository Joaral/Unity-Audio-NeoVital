using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Bala : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D rb;
    protected SpriteRenderer sr;
    private MovementEvan personaje;
    

    public float speed = 5f;
    public int damageType = 0;
    public float damageNormal = 5f;
    public float damageFire = 10f;
    public float damageIce = 7f;
    private Vector2 dirBala;
    //private float evanType;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        personaje = FindObjectOfType<MovementEvan>();

        if (personaje == null)
        {
            Debug.LogError("No se encontró el objeto MovimientoPersonaje en la escena.");
            return;
        }

        if (personaje.horizontalMovement == 0 && personaje.verticalMovement == 0)
        {
            float direccion = personaje.transform.localScale.x;
            if (direccion > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
                dirBala = new Vector2(direccion, 0);
        }
        else
        {
            dirBala = new Vector2(personaje.horizontalMovement, personaje.verticalMovement);
            if (personaje.transform.localScale.x > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }

        rb.velocity = dirBala * speed;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("pared"))
        {
            Destroy(gameObject);
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
