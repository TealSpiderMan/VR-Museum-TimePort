using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabAudioHandler : MonoBehaviour
{
    private AudioSource audioSource;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Subscribe to the grab and release events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (audioSource)
        {
            audioSource.Play();
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (audioSource)
        {
            audioSource.Stop();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
