using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private bool isPlayerInRange;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] public bool cinematicFinished;
    [SerializeField] private bool didDialogueStart;
    [SerializeField] private int lineIndex;

    [SerializeField] private cinematiccontroller cinematicController;


    private float typingTime = 0.05f;

    void Update()
    {
        
    }


    private void StartDialogue()
    {
        didDialogueStart = true;
        dialogueBox.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
        Debug.Log("Iniciando dialogo.");
    }

    private void NextLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialogueBox.SetActive(false);
            if (cinematicController != null)
            {
                cinematicController.PlayNextCinematic();
            }
        }
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entro en el rango de dialogo.");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player salio del rango de dialogo.");
        }
    }
    public void Talk(InputAction.CallbackContext context)
    {
        if (isPlayerInRange && cinematicFinished && context.performed)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }
}
