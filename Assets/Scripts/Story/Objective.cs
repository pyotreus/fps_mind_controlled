using UnityEngine;

[System.Serializable]
public class Objective
{

    public int objectiveId;
    public string description;
    [SerializeField] private ObjectiveState state = ObjectiveState.NotInitialized;
    public enum ObjectiveState
    {
        NotInitialized,
        WaitingForItem,
        Completed
    }

    public void Complete()
    {
        if (!IsComplete())
        {
            state = Objective.ObjectiveState.Completed;
        }
    }

    public ObjectiveState GetState()
    {
        return state;
    }

    public bool IsComplete()
    {
        return state == ObjectiveState.Completed;
    }

    public void SetState(Objective.ObjectiveState objectiveState)
    {
        state = objectiveState;
    }

    private void Start()
    {
        Debug.Log("INITIALIZING OBEJCTIVE " + state);
        state = Objective.ObjectiveState.NotInitialized;
    }

}
