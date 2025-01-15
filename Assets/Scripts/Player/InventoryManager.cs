using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    public GameObject inventoryPanel;
    public GameObject itemNameTemplate;
    public bool crystal;
    private List<StoryEventObject> items = new List<StoryEventObject>();
    private List<GameObject> instantiatedItems = new List<GameObject>();

    public void ToggleInventory()
    {
        bool isActive = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isActive);
    }

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
        UpdateInventoryUI();
    }

    public void AddItem(string itemId)
    {

        StoryEventObject item = StoryEventsObjectsManager.Instance.GetObject(itemId);
        if (item != null)
        {
            items.Add(item);
        }
        UpdateInventoryUI();
    }

    public void RemoveItem(StoryEventObject item)
    {
        items.Remove(item);
        UpdateInventoryUI();
    }

    public void RemoveItemById(string objectId)
    {
        items.RemoveAll(item => item.objectId.Equals(objectId));
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        // Clear previous UI elements
        foreach (GameObject item in instantiatedItems)
        {
            Destroy(item);
        }
        instantiatedItems.Clear();

        // Populate the UI with the current inventory
        foreach (StoryEventObject item in items)
        {
            GameObject newItem = Instantiate(itemNameTemplate, inventoryPanel.transform);
            newItem.GetComponent<TMP_Text>().text = item.objectId; // Use TextMeshPro
            newItem.SetActive(true); // Ensure it's visible
            instantiatedItems.Add(newItem);
        }

    }

    private void Start()
    {
        inventoryPanel.SetActive(false);
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
