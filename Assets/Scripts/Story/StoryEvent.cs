using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryEvent", menuName = "Story/StoryEvent", order = 1)]
[System.Serializable]
public class StoryEvent : ScriptableObject
{

    public int id;
    public string description;
    public List<Objective> objectives;
    public List<string> objectIDsToActivate;
    [SerializeField] private GameObject[] interactableObjects;
    public string taskHint;
    private bool completed => objectives.TrueForAll(objective => objective.completed);

    public void InitializeEvent()
    {
        Debug.Log($"Triggering Story Event: {description}");
        foreach (var objectID in objectIDsToActivate)
        {
            StoryEventObject registerableObject = StoryEventsObjectsManager.Instance.GetObject(objectID);
            
            if (registerableObject != null)
            {
                registerableObject.Activate();
            }
        }
    }

    public bool IsCompleted()
    {
        return completed;
    }
}
