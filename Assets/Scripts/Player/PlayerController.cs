using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform interactPosition;

    private GameObject interactedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInteractedObject(GameObject obj)
    {
        interactedObject = obj;
    }

    public GameObject GetInteractedObject() => interactedObject;

    public Transform GetInteractPosition() => interactPosition;
}
