using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBarrelController : MonoBehaviour
{
    public float maxRotationSpeed = 1000f; // Maximum rotation speed
    public float spinUpTime = 0.5f; // Time to reach maximum speed

    private float currentRotationSpeed = 0f;
    private bool isFiring = false;

    void Update()
    {
        // Check if the player is holding the fire button
        if (Input.GetButton("Fire1"))
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }

        // Adjust rotation speed based on firing state
        if (isFiring)
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, maxRotationSpeed, Time.deltaTime / spinUpTime);
        }
        else
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0f, Time.deltaTime / spinUpTime);
        }

        // Rotate the barrels
        if (currentRotationSpeed > 0)
        {
            RotateBarrels();
        }
    }

    private void RotateBarrels()
    {
        // Rotate the barrel assembly around its local Z-axis
        transform.Rotate(Vector3.forward, currentRotationSpeed * Time.deltaTime);
    }
}
