using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 1000f;
    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    void Start()
    {      
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotation arount x axis (up and down) 
        xRotation -= mouseY;

        //Clamp the rotation 
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Rotation around y axis (left and right)
        yRotation += mouseX;
        
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
