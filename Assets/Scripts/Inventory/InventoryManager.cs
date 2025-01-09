using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    private List<StoryEventObject> items = new List<StoryEventObject>();

    public void AddItem(StoryEventObject item)
    {
        if (item != null && !items.Contains(item))
        {
            items.Add(item);
            Debug.Log("item " + item.objectID);
        }
    }

    public void RemoveItem(StoryEventObject item)
    {
        items.Remove(item);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
