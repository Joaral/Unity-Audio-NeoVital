using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWallController : MonoBehaviour
{
    
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBall"))
        {
            Destroy(gameObject);
        }
    }
}
