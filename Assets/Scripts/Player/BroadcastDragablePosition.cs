using UnityEngine;

public class BroadcastDragablePosition : MonoBehaviour
{
    // References
    [SerializeField] private Transform objectPosition;
    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.GetInteractedObject() != null)
        {
            playerController.GetInteractedObject().BroadcastMessage(BroadcastEvents.ON_DRAGABLE_POSITION, objectPosition.position);
        }
    }
}
