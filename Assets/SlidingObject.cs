using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlidingObject : MonoBehaviour
{
    public float smoothSpeed = 5f; // Speed of movement

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Set Rigidbody properties to restrict movement
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

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
    }

    void FixedUpdate()
    {
        // Lock movement to Y-axis while being grabbed
        Vector3 newPos = transform.position;
        newPos.x = transform.position.x; // Maintain current X position
        newPos.z = transform.position.z; // Maintain current Z position
        transform.position = newPos;
    }
}
