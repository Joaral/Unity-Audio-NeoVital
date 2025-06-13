using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class buttonControler : MonoBehaviour
{
    public string tagCollision = "Player";
    public Animator animator;

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

    [SerializeField] private AudioClip buttonSound;


    void Start()
    {
        animator = GetComponent<Animator>();
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
            animator.SetBool("isPresset", true);
            ControladorSonido.Instance.EjecutarSonido(buttonSound);
        }
    }
    private void ChangeForeground()
    {
        bc.isTrigger = true;
        render.enabled = false;
    }
    private void ChangePlatforms() 
    {
        bc2.isTrigger = false;
        render2.enabled = true;
        if (renderOptional != null)
        {
            renderOptional.enabled = true;
            bcOptional.isTrigger = false;
            renderOptional2.enabled = true;
            bcOptional2.isTrigger = false;
        }
    }
}
