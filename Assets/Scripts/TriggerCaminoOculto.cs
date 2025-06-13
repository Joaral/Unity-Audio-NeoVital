using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerCaminoOculto : MonoBehaviour
{
    public string tagCollision = "Player";
    public TilemapRenderer tilemapRenderer;
    public CompositeCollider2D composite;

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagCollision)
        {
            tilemapRenderer.enabled = false;
            composite.isTrigger = true;
        }
    }
}
