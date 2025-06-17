using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float gravityValue = -4.00f;

    [Header("Camera Look")]
    [SerializeField] private float mouseSensitivity = 70f;
    [SerializeField] private float lookUpLimit = 80f;
    [SerializeField] private float lookDownLimit = -80f;

    // References
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Camera playerCamera;

    // Internal State
    private Vector2 moveInput = new Vector2();
    private Vector2 lookInput = new Vector2();
    private float xRotation = 0f;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playerCamera = gameObject.GetComponentInChildren<Camera>();
        ConfigureCursor();
    }

    void Update()
    {
        HandleMovement();
        HandleCameraLook();
    }

    private void HandleCameraLook()
    {
        // Rotate the player left and right (yaw)
        // CharacterController movement is relative to the player's forward,
        // so we rotate the entire player object for horizontal look.
        transform.Rotate(Vector3.up * lookInput.x * mouseSensitivity * Time.deltaTime);

        // Rotate the camera up and down (pitch)
        xRotation -= lookInput.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, lookDownLimit, lookUpLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        //moveDirection.Normalize();
        Vector3 finalMovement = (moveDirection * playerSpeed) + (Vector3.up * playerVelocity.y);
        controller.Move(finalMovement * Time.deltaTime);
    }

    // Input Broadcast
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (controller.isGrounded)
        {
            playerVelocity.y = jumpHeight;
        }
    }
    
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    private static void ConfigureCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
