using UnityEngine;

public class ItemDeliveryObjectiveStoryEventObject : StoryEventObject
{
    public string objectIdToCollect;
    public string objectIdToDeliver;
    public string textIntroduction;
    public string textProgress;
    public string textOnCompletion;

    private enum ObjectiveState
    {
        NotInitialized,
        WaitingForItem,
        Completed
    }

    private ObjectiveState currentState = ObjectiveState.NotInitialized;

    public override void Interact()
    {
        if (currentState == ObjectiveState.Completed)
        {
            UpdateFeedbackText("You've already completed this objective.");
            return;
        }

        switch (currentState)
        {
            case ObjectiveState.NotInitialized:
                InitializeObjective();
                break;

            case ObjectiveState.WaitingForItem:
                HandleItemDelivery();
                break;
        }
    }

    private void InitializeObjective()
    {
        UpdateFeedbackText(textIntroduction);
        if (!string.IsNullOrEmpty(objectIdToDeliver))
        {
            InventoryManager.Instance.AddItem(objectIdToDeliver);
        }
        
        currentState = ObjectiveState.WaitingForItem;
    }

    private void HandleItemDelivery()
    {
        var itemToCollect = StoryEventsObjectsManager.Instance.GetObject(objectIdToCollect);

        if (InventoryManager.Instance.HasItem(itemToCollect))
        {
            InventoryManager.Instance.RemoveItem(itemToCollect);
            CompleteObjective();
        }
        else 
        {
            UpdateFeedbackText(textProgress);
        }
    }

    private void CompleteObjective()
    {
        StoryManager.Instance.MarkObjectiveComplete(objectiveID);
        currentState = ObjectiveState.Completed;
        UpdateFeedbackText(textOnCompletion);
    }

    private void UpdateFeedbackText(string feedback)
    {
        text = feedback;
        Debug.Log(text);
    }
}
