using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 1000f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float xRotation = 0f;

    [SerializeField] private Joystick cameraJoystick;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        float mouseX = cameraJoystick.Horizontal;
        float mouseY = cameraJoystick.Vertical;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15f, 15f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
