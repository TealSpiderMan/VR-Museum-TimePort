using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BasinTrigger : MonoBehaviour
{
    public UnityEvent OnObjectPlaced;
    public UnityEvent OnObjectRemoved;
    
    private HashSet<GameObject> objectsInBasin = new HashSet<GameObject>();
    private bool isUIActive = false; // Tracks UI state

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidObject(other))
        {
            XRGrabInteractable grabInteractable = other.GetComponent<XRGrabInteractable>();

            if (grabInteractable != null)
            {
                if (grabInteractable.isSelected)
                {
                    grabInteractable.selectExited.AddListener(OnObjectReleased);
                }
                else
                {
                    AddObjectToBasin(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidObject(other))
        {
            XRGrabInteractable grabInteractable = other.GetComponent<XRGrabInteractable>();

            if (grabInteractable != null)
            {
                if (grabInteractable.isSelected)
                {
                    grabInteractable.selectExited.AddListener(OnObjectRemovedAfterRelease);
                }
                else
                {
                    RemoveObjectFromBasin(other.gameObject);
                }
            }
        }
    }

    private void OnObjectReleased(SelectExitEventArgs args)
    {
        AddObjectToBasin(args.interactableObject.transform.gameObject);
    }

    private void OnObjectRemovedAfterRelease(SelectExitEventArgs args)
    {
        RemoveObjectFromBasin(args.interactableObject.transform.gameObject);
    }

    private void AddObjectToBasin(GameObject obj)
    {
        if (obj.name.Contains("ActivatorSphere"))
        {
            if (!objectsInBasin.Contains(obj))
            {
                objectsInBasin.Add(obj);
                Debug.Log($"{obj.name} placed in the basin.");

                if (!isUIActive) // Ensure it only activates once
                {
                    isUIActive = true;
                    OnObjectPlaced?.Invoke();
                }
            }
        }
    }

    private void RemoveObjectFromBasin(GameObject obj)
    {
        if (obj.name.Contains("ActivatorSphere"))
        {
            if (objectsInBasin.Contains(obj))
            {
                objectsInBasin.Remove(obj);
                Debug.Log($"{obj.name} removed from the basin.");

                if (objectsInBasin.Count == 0 && isUIActive) // Only trigger when the last object is removed
                {
                    isUIActive = false;
                    OnObjectRemoved?.Invoke();
                }
            }
        }
    }

    private bool IsValidObject(Collider other)
    {
        return other.GetComponent<XRGrabInteractable>() || other.attachedRigidbody;
    }
}
