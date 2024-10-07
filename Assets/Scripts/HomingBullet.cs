using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public Transform target;  // The target to home in on
    public float turnSpeed = 10f;  // How fast the bullet turns toward the target
    public float bulletSpeed = 20f;  // Speed of the bullet
    public float maxHomingAngle = 90f;  // Maximum allowed angle to keep homing
    private Rigidbody rb;
    private bool isHoming = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null || !isHoming)
        {
            // No target or stopped homing, so keep going straight
            return;
        }

        // Calculate the direction toward the target
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // Calculate the current angle between the bullet's forward direction and the target direction
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        // If the angle is too large, stop homing and just continue flying in the current direction
        if (angleToTarget > maxHomingAngle)
        {
            isHoming = false;
            return;  // Stop homing behavior
        }

        // Smoothly rotate the bullet's forward direction towards the target
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToTarget, turnSpeed * Time.deltaTime, 0.0f);

        // Update the bullet's velocity to match the new direction
        rb.velocity = newDirection * bulletSpeed;

        // Update the bullet's transform to match the direction it's flying
        transform.forward = newDirection;
    }
}