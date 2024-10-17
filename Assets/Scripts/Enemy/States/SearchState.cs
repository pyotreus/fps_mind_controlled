using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchState : BaseState
{
    private float searchTimer;
    public float moveTimer;
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.PlayersLastKnownPosition);
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }

        if (enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            if (moveTimer > Random.Range(3, 5))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
            }
            if (searchTimer > 10)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }

        // Traverse Off-Mesh Link for jump/gap traversal without using Coroutine
        //if (enemy.Agent.isOnOffMeshLink)
        //{
        //    TraverseOffMeshLink();
        //}
    }

    private void TraverseOffMeshLink()
    {
        enemy.Agent.autoTraverseOffMeshLink = false;
        // Get the start and end points of the link
        OffMeshLinkData linkData = enemy.Agent.currentOffMeshLinkData;
        Vector3 startPos = linkData.startPos;
        Vector3 endPos = linkData.endPos;

        // You can customize the duration of the jump
        float jumpDuration = 1.0f;
        float jumpHeight = 3.0f;
        float time = 0f;

        // While traversing, smoothly interpolate between the start and end positions
        while (time < jumpDuration)
        {
            // Calculate jump curve (parabolic)
            float t = time / jumpDuration;
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);
            currentPos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

            enemy.Agent.transform.position = currentPos;
            time += Time.deltaTime;
        }

        // Ensure the agent lands exactly at the endpoint
        enemy.Agent.transform.position = endPos;

        // Complete the Off-Mesh Link
        enemy.Agent.CompleteOffMeshLink();
        enemy.Agent.autoTraverseOffMeshLink = true;
    }
}
