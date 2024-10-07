using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.VersionControl;
using UnityEngine;

public class PatrolState : BaseState
{
    public int wayPointIndex;
    public float waitTimer;

    public override void Enter()
    {
        //throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        //throw new System.NotImplementedException();
    }

    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            Debug.Log("Changing state to attack");
            stateMachine.ChangeState(new AttackState());
        } 

        
    }

    public void PatrolCycle()
    {
        waitTimer = Time.deltaTime;
        //if (waitTimer > 3)
        //{
            if (enemy.Agent.remainingDistance < 0.2f)
            {
                if (wayPointIndex < enemy.path.wayPoints.Count - 1)
                {
                    wayPointIndex++;
                } else
                {
                    wayPointIndex = 0;
                }
                enemy.Agent.SetDestination(enemy.path.wayPoints[wayPointIndex].position);
                waitTimer = 0;
            //} 
        }
    }

}
