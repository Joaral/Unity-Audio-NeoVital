using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausa();
        }
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Cerrar()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
