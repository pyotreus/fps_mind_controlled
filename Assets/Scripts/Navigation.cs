using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    // Rotation offset in degrees (e.g., 90 if model faces +X instead of +Z)
    public float rotationOffset = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = player.position;
        //RotateTowardsMovementDirection();
    }

    void RotateTowardsMovementDirection()
    {
        Vector3 velocity = agent.velocity;
        // Ignore vertical movement
        velocity.y = 60;

        if (velocity.sqrMagnitude > 0.1f)
        {
            // Calculate the desired rotation based on movement direction
            Quaternion desiredRotation = Quaternion.LookRotation(velocity.normalized, Vector3.up);
            // Apply the rotation offset
            desiredRotation *= Quaternion.Euler(0, rotationOffset, 0);
            // Smoothly rotate towards the desired rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 5f);
        }
    }
}
