using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private string[] messages;
    private int currentMessageIndex;

    private bool dialogueActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void ShowDialogue(string message)
    {
        dialogueActive = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = message;
    }

    public void ShowDialogue(string[] messageArray)
    {
        dialogueActive = true;
        dialoguePanel.SetActive(true);
        messages = messageArray;
        currentMessageIndex = 0;
        ShowNextMessage();
    }

    public void ShowNextMessage()
    {
        if (messages != null && currentMessageIndex < messages.Length)
        {
            dialogueText.text = messages[currentMessageIndex];
            currentMessageIndex++;
        }
        else
        {
            ClearDialogue();
        }
    }

    public void ClearDialogue()
    {
        dialogueText.text = "";
        messages = null;
        currentMessageIndex = 0;
        dialoguePanel.SetActive(false);
        dialogueActive = false;
    }

    public bool IsDialogueActive() { 
        return dialogueActive;
    }

    private void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextMessage();
        }
    }
}
