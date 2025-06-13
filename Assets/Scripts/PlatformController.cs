using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public PlatformEffector2D pE2D;

    public bool leftPlatform = false;

    void Start()
    {
        pE2D.GetComponent<PlatformEffector2D>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !leftPlatform)
        {
            pE2D.rotationalOffset = 0;
            
            leftPlatform = true;

            gameObject.layer = 2;
        }    
    }

    private void onCollisionExit2D(CompositeCollider2D other)
    {
        pE2D.rotationalOffset = 180;

        leftPlatform = false;

        gameObject.layer = 0;
    }
}
