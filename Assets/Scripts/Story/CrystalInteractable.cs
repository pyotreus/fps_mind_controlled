using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalInteractable : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnInteract;

    public void Interact()
    {
        OnInteract?.Invoke();
    }
}
