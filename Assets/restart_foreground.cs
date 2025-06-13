using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class restart_foreground : MonoBehaviour
{
    public string tagCollision = "Player";

    [Header("ForegroundControler")]
    public CompositeCollider2D bc;
    public CompositeCollider2D bc2;

    [Header("PlatformControler")]
    public TilemapRenderer render;
    public TilemapRenderer render2;

    [Header("ObjectsOptional")]
    public SpriteRenderer renderOptional;
    public SpriteRenderer renderOptional2;
    public BoxCollider2D bcOptional;
    public BoxCollider2D bcOptional2;

    void Start()
    {
        bc.GetComponent<BoxCollider2D>();
        bc2.GetComponent<BoxCollider2D>();
        bcOptional.GetComponent<BoxCollider2D>();
        bcOptional2.GetComponent<BoxCollider2D>();
        render.GetComponent<TilemapRenderer>();
        render2.GetComponent<TilemapRenderer>();
        renderOptional.GetComponent<SpriteRenderer>();
        renderOptional2.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == tagCollision)
        {
            ChangeForeground();
            ChangePlatforms();
        }
    }
    private void ChangeForeground()
    {
        bc.isTrigger = false;
        render.enabled = true;
    }
    private void ChangePlatforms()
    {
        bc2.isTrigger = true;
        render2.enabled = false;
        if (renderOptional != null)
        {
            renderOptional.enabled = true;
            bcOptional.isTrigger = false;
            renderOptional2.enabled = true;
            bcOptional2.isTrigger = false;
        }
    }
}
