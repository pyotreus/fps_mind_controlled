using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEventObject : MonoBehaviour, IStoryEventTarget
{
    public int objectiveID;
    public string objectID;
    public bool collectable;

    public virtual void Activate()
    {
        //Debug.Log($"{name} interacted with!");
    }

    public virtual void Interact()
    {
        Debug.Log($"{name} interacted with!");
        if (collectable)
        {
            InventoryManager.Instance.AddItem(this);
            gameObject.SetActive(false);
        }
        StoryManager.Instance.MarkObjectiveComplete(objectiveID);       
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
