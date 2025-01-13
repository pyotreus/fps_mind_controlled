using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    public bool crystal;
    private List<StoryEventObject> items = new List<StoryEventObject>();

    public bool HasItem(StoryEventObject item)
    {
        return items.Any(i => i.objectId == item.objectId);
    }

    public bool HasItem(string item)
    {
        return items.Any(i => i.objectId == item);
    }

    public List<StoryEventObject> GetItems()
    {
        return items;
    }

    public void AddItem(StoryEventObject item)
    {
        if (item != null && !items.Contains(item))
        {
            items.Add(item);
        }
    }

    public void AddItem(string itemId)
    {

        StoryEventObject item = StoryEventsObjectsManager.Instance.GetObject(itemId);
        if (item != null)
        {
            items.Add(item);
        }
    }

    public void RemoveItem(StoryEventObject item)
    {
        items.Remove(item);
    }

    public void RemoveItemById(string objectId)
    {
        items.RemoveAll(item => item.objectId.Equals(objectId));
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
