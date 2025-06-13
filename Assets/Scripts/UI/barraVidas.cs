using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barraVidas : MonoBehaviour
{
    public GameObject[] vidas;
    public GameObject[] vidasVacias;
    public RectTransform cara;

    void Start()
    {
        Vector2[] posiciones = {
        new Vector2(207, -118),
        new Vector2(417, -183),
        new Vector2(564, -183)
        };

        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].GetComponent<RectTransform>().anchoredPosition = posiciones[i];
            vidasVacias[i].GetComponent<RectTransform>().anchoredPosition = posiciones[i];
        }
        cara.anchoredPosition = new Vector2(135.5f, -118f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DesactivarVida(int indice)
    {
        vidas[indice].SetActive(false);
    }
    public void ActivarVida(int indice)
    {
        vidas[indice].SetActive(true);
    }

    public void DesactivarVidaVacia(int indice)
    {
        vidasVacias[indice].SetActive(false);
    }
    public void ActivarVidaVacia(int indice)
    {
        vidasVacias[indice].SetActive(true);
    }
}
