using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 1000f;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _xRotation = 0f;
    [SerializeField] private Joystick _cameraJoystick;
    private bool _isMobile;
    private float _mouseX;
    private float _mouseY;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _isMobile = Application.isMobilePlatform;
        
    }

    void Update()
    {
        if (_isMobile)
        {
            _mouseX = _cameraJoystick.Horizontal;
            _mouseY = _cameraJoystick.Vertical;
        }
        else
        {
            _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            _mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        }

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -15f, 15f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        _playerBody.Rotate(Vector3.up * _mouseX);
    }
}
