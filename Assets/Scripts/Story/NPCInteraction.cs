using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [TextArea] public string npcDialogue;
    [TextArea] public string[] dialogueMessages;

    public void Interact()
    {
        if (dialogueMessages != null && dialogueMessages.Length > 0)
        {
            DialogueManager.Instance.ShowDialogue(dialogueMessages);
        }
        else if (!string.IsNullOrEmpty(npcDialogue))
        {
            DialogueManager.Instance.ShowDialogue(npcDialogue);
        }
        else
        {
            Debug.LogWarning("No dialogue set for this NPC.");
        }
    }
}
