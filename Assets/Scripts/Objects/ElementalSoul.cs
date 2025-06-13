using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElementalSoul : MonoBehaviour
{
    [Header("Soul Settings")]
    public float soulType;
    public int ammoAmount = 4;
    public string tagTarget;
    Animator animator;
    float timer = 0f;
    bool timerActive = false;

    [SerializeField] private AudioClip fireSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timerActive)
        {
            timer += Time.deltaTime;
            Debug.Log("timer: " + timer);
        }
        if (timer >= 0.15f)
        {
            
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (string.IsNullOrWhiteSpace(tagTarget) || collision.CompareTag(tagTarget))
        {
            collision.GetComponent<ShootingEvan>().SetElement(soulType, ammoAmount);
            animator.SetBool("isGrabbed", true);
            ControladorSonido.Instance.EjecutarSonido(fireSound);
            timerActive = true;
            
        }
    }

  
}
