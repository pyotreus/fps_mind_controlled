using nl.ma.utopiaserver.messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public float moveSpeed, distanceToTarget;

    private Vector3 target;

    public NavMeshAgent agent;

    public GameObject bullet;
    public Transform bulletSpawn;

    public float fireRate;
    private float fireCount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update target position
        target = PlayerMovement.instance.transform.position;

        // Set destination for NavMeshAgent
        agent.destination = target;

        // Stop moving if within distance
        if (Vector3.Distance(transform.position, target) <= distanceToTarget)
        {
            agent.destination = transform.position;
        }

        // Handle firing
        fireCount -= Time.deltaTime;
        if (fireCount <= 0)
        {
            fireCount = fireRate;

            bulletSpawn.LookAt(PlayerMovement.instance.transform.position + new Vector3(0, 1.5f, 0));

            Vector3 targetDirection = PlayerMovement.instance.transform.position - transform.position;

            float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

            if (Mathf.Abs(angle) < 45f)
            {
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

            }
            else
            {
                agent.destination = target;
            }
        }

        // Adjust rotation to face the player correctly
        //AdjustRotation();
    }

    void AdjustRotation()
    {
        Vector3 direction = target - transform.position;
        direction.y = 0; // Keep the enemy upright

        if (direction != Vector3.zero)
        {
            // Calculate the desired rotation
            Quaternion desiredRotation = Quaternion.LookRotation(direction);

            // Apply an offset to compensate for the model's initial rotation
            // Since the model faces Z, and we want it to face X, rotate by -90 degrees on Y
            Quaternion offsetRotation = Quaternion.Euler(0, -90, 0);
            desiredRotation *= offsetRotation;

            // Smoothly rotate towards the desired rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 5f);
        }
    }
}
