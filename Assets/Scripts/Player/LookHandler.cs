using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionLabel;
    [SerializeField] private float MaxDistanceView = 0.50f;
    [SerializeField] private LayerMask mask;

    private Camera playerCamera;
    private GameObject lookinObject;
    public float raycastOffset = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(screenCenter);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        // This offset is necessary because if raycast is too close of camera this hit itself and dont propagate even if use layer maskss
        Vector3 rayOrigin = ray.origin + ray.direction * raycastOffset;
        Ray finalRay = new(rayOrigin, ray.direction);

        if (Physics.Raycast(finalRay, out hit, MaxDistanceView, mask))
        {
            Transform objectHit = hit.transform;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

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
