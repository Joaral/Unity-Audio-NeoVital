using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class cinematiccontroller : MonoBehaviour
{
    public PlayableDirector director;
    public Dialogue dialogue;
    public PlayableDirector nextDirector;
    public string nextScene; // Nombre de la siguiente escena

    void Start()
    {
        director.Play();
        if (nextDirector != null)
            nextDirector.stopped += OnNextCinematicEnd;
    }

    public void OnCinematicEnd()
    {
        dialogue.cinematicFinished = true;
    }

    public void PlayNextCinematic()
    {
        if (nextDirector != null)
            nextDirector.Play();
    }

    private void OnNextCinematicEnd(PlayableDirector obj)
    {
        if (!string.IsNullOrEmpty(nextScene))
            SceneManager.LoadScene(nextScene);
    }
}
