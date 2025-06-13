using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControler : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D bc;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public string tagTarget;

    private bool startMoving = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        if (startMoving)
        {
            rb.velocity = new Vector3(1, rb.velocity.y, 0) * 1.3f;
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }


    public void newTrigger(string collisionString)
    {
        if (tagTarget == collisionString)
        {
            animator.SetTrigger("LookEvan");
            startMoving = true;
            sr.flipX = true;
        }
    }


}
