using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoggyStoryEventObject : StoryEventObject
{

    public string requiredItemId;
    public Transform player;
    public Transform destination;
    public float followDistance = 2f;
    public string textWhenFollowing;
    public string textWhenNoItem;
    public string textOnCompletion;

    private bool isFollowing = false;
    private NavMeshAgent navMeshAgent;

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
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    public override void Interact()
    {
        if (isFollowing)
        {
            UpdateFeedbackText("The dog is already following you.");
            return;
        }
        Debug.Log("has item " + InventoryManager.Instance.HasItem(requiredItemId));

        if (InventoryManager.Instance.HasItem(requiredItemId))
        {
            StartFollowing();
        }
        else
        {
            UpdateFeedbackText(textWhenNoItem);
        }
    }

    private void StartFollowing()
    {
        isFollowing = true;
        UpdateFeedbackText(textWhenFollowing);
    }

    private void UpdateFeedbackText(string feedback)
    {
        Debug.Log(feedback);
    }

    private void Update()
    {
        if (isFollowing)
        {
            if (Vector3.Distance(transform.position, player.position) > followDistance)
            {
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                navMeshAgent.ResetPath(); // Stop moving if close enough
            }

            //if (Vector3.Distance(transform.position, destination.position) < 1f)
            //{
            //    CompleteQuest();
            //}
        }
    }

    //private void CompleteQuest()
    //{
    //    isFollowing = false;
    //    navMeshAgent.ResetPath();
    //    StoryManager.Instance.MarkObjectiveComplete(objectiveID);
    //    UpdateFeedbackText(textOnCompletion);
    //}

}
