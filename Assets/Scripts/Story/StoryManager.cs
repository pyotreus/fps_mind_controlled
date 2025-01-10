using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{

    public static StoryManager Instance;
    public List<StoryEvent> storyEvents;
    private int currentEventIndex = 0;
    private StoryEvent CurrentEvent => storyEvents[currentEventIndex];

    public string GetCurrentTaskHint()
    {
        return storyEvents[currentEventIndex].taskHint;
    }

    public void MarkObjectiveComplete(int objectiveId)
    {
        Objective objective = CurrentEvent.objectives.Find(objective => objective.objectiveId == objectiveId);
        if (objective != null)
        {            
            objective.Complete();
            AdvanceStory();
        }
    }

    public bool IsCurrentEventObjectiveComplete(int objectiveId)
    {
        Objective objective = CurrentEvent.objectives.Find(objective => objective.objectiveId == objectiveId);
        if (objective != null)
        {
            return objective.IsComplete();
        } 
        return false;
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

    private void Start()
    {
        if (CurrentEvent != null)
        {
            CurrentEvent.InitializeEvent();
        }
    }

    private void AdvanceStory()
    {
        if (CurrentEvent.IsCompleted() && currentEventIndex < storyEvents.Count - 1)
        {
            currentEventIndex++;
            CurrentEvent.InitializeEvent();
        } else if (currentEventIndex == storyEvents.Count - 1)
        {
            Debug.Log("Story is finished");
        }
    }

}
