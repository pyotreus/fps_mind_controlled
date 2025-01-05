using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public int objectiveId;
    public string InteractionPrompt = "Press E to interact";
    public UnityEngine.Events.UnityEvent OnInteract;

    public void Interact()
    {
        OnInteract?.Invoke();
    }

    public void Activate()
    {
        StoryManager.Instance.MarkObjectiveComplete(objectiveId);
    }
}
