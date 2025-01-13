using UnityEngine;

public class ItemDeliveryObjectiveStoryEventObject : StoryEventObject
{
    public string objectIdToCollect;
    public string objectIdToDeliver;
    public string textIntroduction;
    public string textProgress;
    public string textOnCompletion;

    public override void Interact()
    {
        //Debug.Log("objective.GetState() " + objective.description);
        if (StoryManager.Instance.IsCurrentEventObjectiveComplete(objectiveId))
        {
            UpdateFeedbackText("You've already completed this objective.");
            return;
        }

        switch (objective.GetState())
        {
            case Objective.ObjectiveState.NotInitialized:
                InitializeObjective();
                break;

            case Objective.ObjectiveState.WaitingForItem:
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

        GetObjective().SetState(Objective.ObjectiveState.WaitingForItem);
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
        StoryManager.Instance.MarkObjectiveComplete(objectiveId);
        UpdateFeedbackText(textOnCompletion);
    }

    private void UpdateFeedbackText(string feedback)
    {
        DialogueManager.Instance.ShowDialogue(feedback);
        text = feedback;
        Debug.Log(text);
    }
}
