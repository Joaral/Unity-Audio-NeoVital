using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    private Rigidbody2D rb;
    private MovimientoPatrulla patrulla;

    public float speed = 5f;
    public int tipoDeDano = 0;
    public float damageNormal = 5f;
    public float damageFire = 10f;
    public float damageIce = 7f;

    private Vector2 dirBala;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //patrulla = FindObjectOfType<MovimientoPatrulla>();

        //if (patrulla == null)
        //{
        //    Debug.LogError("No se encontró el objeto MovimientoPatrulla en la escena.");
        //    return;
        //}

        dirBala = -transform.right;
        rb.velocity = dirBala * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pared"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            return;
        }

    }
}
