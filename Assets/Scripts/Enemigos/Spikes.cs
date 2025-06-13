using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.CompareTag("Player")) 
        {
            Destroy(trigger.gameObject);
            Debug.Log("El jugador a muerto por culpa de los spikes.");
            SceneManager.LoadScene("Zone_1_Joan");
        }
    }
}
