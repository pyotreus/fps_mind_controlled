using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string InteractionPrompt = "Press E to interact";
    public UnityEngine.Events.UnityEvent OnInteract;

    public void Interact()
    {
        OnInteract?.Invoke();
    }

}
