using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cargadorBalas : MonoBehaviour
{
    public GameObject[] balas;



    void Start()
    {
        //for (int i = 0; i < balas.Length; i++)
        //{
        //    balas[i].GetComponent<RectTransform>().anchoredPosition = posiciones[i];
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DesactivarBala(int indice)
    {
        balas[indice].SetActive(false);
    }
    public void ActivarBala(int indice, float type)
    {
        balas[indice].SetActive(true);
        Image img = balas[indice].GetComponent<Image>();
        if (type == 1)
        {
            img.color = new Color(1f, 0.3f, 0f);
        } else
        {
            img.color = new Color(0f, 0.7f, 1f);
        }
    }
}
