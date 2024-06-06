using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Controller Settings")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _jumpHeight = 8f;
    [SerializeField] private float _speed = 6f;
    
    [Header("Camera Settings")]
    [SerializeField] private float _mouseSensitivity = 1f;
    private Camera _mainCamera;
    private Vector3 _velocity;
    private const float _gravity = 20f;
    
    void Start()
    {
        if (_characterController == null) _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
        if (_mainCamera == null) throw new ArgumentNullException(_mainCamera.name);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CalculateMovement();
        CameraController();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    private void CameraController()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        var currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        var cameraRotation =  _mainCamera.transform.localEulerAngles;
        cameraRotation.x -= mouseY * _mouseSensitivity;
        _mainCamera.transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(cameraRotation.x, 0, 27), Vector3.right);
    }

    private void CalculateMovement()
    {
        if (_characterController.isGrounded)
        {
            _velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }
        }

        _velocity.y -= _gravity * Time.deltaTime;
        _velocity = transform.TransformDirection(_velocity);
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
