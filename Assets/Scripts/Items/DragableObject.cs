using System;
using Unity.VisualScripting;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    //References
    private Rigidbody rb;

    //State
    private bool isActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInteractPosition();
        HandleRigidbodyKitematic();
    }

    private void UpdateInteractPosition()
    {
        if (isActive)
        {
            transform.position = WorldInfo.Instance.GetPlayer().GetInteractPosition().position;
        }
    }

    private void HandleRigidbodyKitematic()
    {
        rb.isKinematic = isActive;
    }

    // Message
    void OnLookAndInteract(){
        isActive = !isActive;
    }
}
