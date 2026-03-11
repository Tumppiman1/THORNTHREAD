using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;   // <-- FIX
    public TextMeshProUGUI textComponent;
    public float textSpeed = 0.02f;

    private string[] lines;
    private int index;

    public bool DialogueOpen { get; private set; }

    public event Action onDialogueFinished;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        textComponent.text = "";
        dialoguePanel.SetActive(false);  // <-- FIX
    }

    public void StartDialogue(DialogueData data)
    {
        Debug.Log("StartDialogue CALLED!"); // debugging

        lines = data.lines;
        index = 0;

        DialogueOpen = true;

        textComponent.text = "";
        dialoguePanel.SetActive(true);  // <-- FIX

        StartCoroutine(TypeLine());
    }

    private void Update()
    {
        if (!DialogueOpen) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            DialogueOpen = false;
            dialoguePanel.SetActive(false);  // <-- FIX
            onDialogueFinished?.Invoke();
        }
    }
}
