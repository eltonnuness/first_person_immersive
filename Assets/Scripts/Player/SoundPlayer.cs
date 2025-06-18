using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private GameObject footstepsGO;

    private bool isWalking;

    private void Update()
    {
        footstepsGO.SetActive(isWalking);
    }

    public void OnMove(InputValue value)
    {
        isWalking = value.Get<Vector2>() != Vector2.zero;
    }
}
