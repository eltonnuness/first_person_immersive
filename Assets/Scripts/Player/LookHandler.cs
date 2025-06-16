using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionLabel;
    [SerializeField] private float MaxDistanceView = 0.35f;

    private Camera playerCamera;
    private GameObject lookinObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, MaxDistanceView))
        {
            Transform objectHit = hit.transform;

            if (objectHit.CompareTag("Item"))
            {
                descriptionLabel.text = objectHit.name;
                lookinObject = objectHit.gameObject;
            } else
            {
                descriptionLabel.text = "";
                lookinObject = null;
            }
        } else
        {
            descriptionLabel.text = "";
            lookinObject = null;
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            if (lookinObject != null)
            {
                lookinObject.BroadcastMessage("OnMessage");
            }
        }
    }
}
