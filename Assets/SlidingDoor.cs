using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SlidingDoor : MonoBehaviour
{
    public Transform startLimit; // Leftmost limit
    public Transform endLimit;   // Rightmost limit
    public float smoothSpeed = 5f; // Speed of movement
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Set Rigidbody properties to restrict movement
        rb.isKinematic = false; // Allow controlled movement
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        // Subscribe to grab events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        rb.isKinematic = false; // Allow controlled movement while grabbed
    }

    void OnRelease(SelectExitEventArgs args)
    {
        rb.isKinematic = true; // Prevent free movement when released
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, startLimit.position.x, endLimit.position.x);
        transform.position = clampedPosition; // Ensure it stays in the limits
    }

    void FixedUpdate()
    {
        // Lock movement to X-axis while being grabbed
        Vector3 newPos = transform.position;
        newPos.y = startLimit.position.y; // Lock Y
        newPos.z = startLimit.position.z; // Lock Z
        transform.position = newPos;
    }
}
