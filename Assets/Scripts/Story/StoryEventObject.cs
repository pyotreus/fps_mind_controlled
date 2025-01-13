using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEventObject : MonoBehaviour, IStoryEventTarget
{
    public string storyEventId;
    public int objectiveId;
    public string objectId;
    public bool collectable;
    public bool NPC;
    public string text;
    protected Objective objective;

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
        if (NPC)
        {
            Debug.Log(text);
        }      
    }

    public Objective GetObjective()
    {
        return objective;
    }

    public void Start()
    {
        if (StoryEventsObjectsManager.Instance != null)
        {
            StoryEventsObjectsManager.Instance.RegisterObject(this);
        }
        else
        {
            Debug.LogWarning("StoryEventsObjectsManager not available in the scene yet!");
        }
        objective = StoryManager.Instance.FindObjectiveByEventIdAndObjectiveId(storyEventId, objectiveId);
        if (objective == null)
        {
            Debug.LogError("Objective not found!");
        }

    }
}
