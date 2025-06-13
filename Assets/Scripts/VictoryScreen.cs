using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("PlayGround");
    }
}