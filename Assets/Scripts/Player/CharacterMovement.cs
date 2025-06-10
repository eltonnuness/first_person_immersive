using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float mouseSensivity = 100.0f;

    private Vector2 moveInput = new Vector2();
    private float jumpInput = 0.0f;
    private Vector2 lookInput = new Vector2();

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleCameraLook();
    }

    private void HandleCameraLook()
    {
        Cursor.lockState = CursorLockMode.Locked;
        float mouseX = lookInput.x * mouseSensivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Jump
        if (jumpInput != 0.0f && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(finalMove * playerSpeed * Time.deltaTime);
    }

    // Input Broadcast
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        jumpInput = value.Get<float>();
    }
    
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

}
