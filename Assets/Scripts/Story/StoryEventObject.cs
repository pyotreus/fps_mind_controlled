using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEventObject : MonoBehaviour, IInteractable
{
    public string objectID;

    public virtual void Interact()
    {
        Debug.Log($"{name} interacted with!");
    }

    private void Start()
    {
        if (StoryEventsObjectsManager.Instance != null)
        {
            StoryEventsObjectsManager.Instance.RegisterObject(this);
        }
        else
        {
            Debug.LogWarning("StoryEventsObjectsManager not available in the scene yet!");
        }
    }
}
