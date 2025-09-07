using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ToggleMaterialOnHover : MonoBehaviour
{
    public Material light3Material; // Assign in Inspector
    private Material originalMaterial;
    private Renderer objectRenderer;
    private bool isSwapped = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }

        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.hoverExited.AddListener(OnHoverExit);
            interactable.selectEntered.AddListener(OnSelectEnter);
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (objectRenderer != null)
        {
            objectRenderer.material = light3Material;
        }
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        if (!isSwapped && objectRenderer != null)
        {
            objectRenderer.material = originalMaterial;
        }
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        isSwapped = !isSwapped;
        objectRenderer.material = isSwapped ? light3Material : originalMaterial;
    }
}
