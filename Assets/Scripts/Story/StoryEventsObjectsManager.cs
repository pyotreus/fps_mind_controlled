using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEventsObjectsManager : MonoBehaviour
{
    public static StoryEventsObjectsManager Instance { get; private set; }

    private Dictionary<string, StoryEventObject> objectDictionary = new Dictionary<string, StoryEventObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RegisterObject(StoryEventObject obj)
    {
        if (!objectDictionary.ContainsKey(obj.objectID))
        {
            objectDictionary.Add(obj.objectID, obj);
        }
    }

    public StoryEventObject GetObject(string objectID)
    {
        if (objectDictionary.TryGetValue(objectID, out var obj))
        {
            return obj;
        }
        else
        {
            Debug.LogError($"Object with ID '{objectID}' not found in StoryEventsObjectsManager.");
            return null;
        }
    }
}
