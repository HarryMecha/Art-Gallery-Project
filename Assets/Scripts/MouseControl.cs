using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {

    public float sensitivity = 100f; //This will be the mouse's default sensitivity

    public Transform userBody; //This is the user GameObject

    float xRotation = 0f; //This will be the default for the xRotation

    public Crosshair ch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //This will hide and lock the cursor to the middle of the window
    }

    private void Update()
    {
        if (ch.movement == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; //This will set the float mouse X to the mouse's currentposition, setting it's speed via sensitivity and mkaing sure it moves independent of the frame rate useing Time
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;//This will set the float mouse Y to the mouse's currentposition, setting it's speed via sensitivity and mkaing sure it moves independent of the frame rate useing Time

            xRotation -= mouseY; //Every frame it will decrease our xrotation by the mouse input in the y direction
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); //This will clamp the rotation so it doesn't suprass directly up and down

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //This will rotate the camera in the x axis
            userBody.Rotate(Vector3.up * mouseX); //This will rotate around the y axis of the user GameObject (Looking side to side)
        }
    }
}
