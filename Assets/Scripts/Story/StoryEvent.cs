using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryEvent", menuName = "Story/StoryEvent", order = 1)]
[System.Serializable]
public class StoryEvent : ScriptableObject
{

    public string id;
    public string description;
    public List<Objective> objectives;
    public List<string> objectIdsToActivate;
    public List<string> objectIdsToDeactivate;
    [SerializeField] private GameObject[] interactableObjects;
    public string taskHint;
    [SerializeField] private bool completed => objectives.TrueForAll(objective => objective.IsComplete());

    public void InitializeEvent()
    {
        Debug.Log($"Triggering Story Event: {description}");
        foreach (var objectID in objectIdsToActivate)
        {
            StoryEventObject registerableObject = StoryEventsObjectsManager.Instance.GetObject(objectID);
            
            if (registerableObject != null)
            {
                //registerableObject.Activate();
                registerableObject.gameObject.SetActive(true);
            }
        }
        foreach (var objectID in objectIdsToDeactivate)
        {
            StoryEventObject registerableObject = StoryEventsObjectsManager.Instance.GetObject(objectID);

            if (registerableObject != null)
            {
                registerableObject.gameObject.SetActive(false);
            }
        }
    }

    public bool IsCompleted()
    {
        Debug.Log("completed " + completed);
        return completed;
    }
}
