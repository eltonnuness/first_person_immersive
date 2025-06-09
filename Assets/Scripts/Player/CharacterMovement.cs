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
        Vector2 lookResult = new Vector3(lookInput.y, lookInput.x, 0) * Time.deltaTime * mouseSensivity;

        transform.Rotate(lookResult);
    }

    private void HandleMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (moveInput != Vector2.zero)
        {
            //transform.forward = moveInput;
        }

        // Jump
        if (jumpInput != 0.0f && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = (new Vector3(moveInput.x, 0, moveInput.y) * playerSpeed);
        controller.Move(finalMove * Time.deltaTime);
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
