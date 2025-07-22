using UnityEngine;

public class DeliveryTask : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLookAndInteract()
    {
        WorldInfo.Instance.GetGameDirector().VerifyTask();
    }
}
