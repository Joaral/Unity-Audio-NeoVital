using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;

    [Header("VariablesPublicas")]
    public float evanType;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        evanType = 0;

    }

    
    void Update()
    {
        if (evanType == 0)
        {
            //Neutral
        }
        else if (evanType == 1)
        {
            //Fire
        }
        else if (evanType == 2)
        {
            //Ice
        }
    }
}
