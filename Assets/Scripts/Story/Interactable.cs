using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public string InteractionPrompt = "Press E to interact";
    public UnityEngine.Events.UnityEvent OnInteract;

    public void Interact()
    {
        OnInteract?.Invoke();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
