using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SFXManagerUI : MonoBehaviour
{
    public static SFXManagerUI Instance;

    public AudioSource audioSource;
    public AudioClip hoverClip;
    public AudioClip clickClip;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayHoverSound()
    {
        if (hoverClip != null)
            audioSource.PlayOneShot(hoverClip);
    }

    public void PlayClickSound()
    {
        if (clickClip != null)
            audioSource.PlayOneShot(clickClip);
    }
}
