using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class UIButtonSFX : MonoBehaviour
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("SonidoHover");
        SFXManagerUI.Instance?.PlayHoverSound();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("SonidoClick");
        SFXManagerUI.Instance?.PlayClickSound();
    }
}
