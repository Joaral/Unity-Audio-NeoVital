using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    public string tagCollision = "Player";
    public string nextScene;
    public Animator animator;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagCollision)
        {
            animator.SetBool("Opening", true);

            //if (Input.GetKeyDown(KeyCode.UpArrow))
            //{
            //    SceneManager.LoadScene(nextScene);
            //}
            SceneManager.LoadScene(nextScene);

        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == tagCollision)
        {
            animator.SetBool("Opening", false);
        }
    }

}

  
