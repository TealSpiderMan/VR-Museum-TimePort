using UnityEngine;
using UnityEngine.UI;

public class ToggleMaterialSwitch : MonoBehaviour
{
    public Material lightMaterial;  // Assign "Light 2"
    public Material glassMaterial;  // Assign "Glass 2"
    public Toggle toggle;           // Assign the Toggle UI element

    private bool isGlass = false;

    void Start()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(OnToggleChanged); 
        }
        else
        {
            Debug.LogError("Toggle is not assigned!");
        }
    }

    private void OnToggleChanged(bool isOn)
    {
        isGlass = isOn;
        ToggleMaterial();
    }

    private void ToggleMaterial()
    {
        bool materialChanged = false;

        // Find all objects with a Renderer component in the scene
        Renderer[] allRenderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in allRenderers)
        {
            Material[] materials = renderer.sharedMaterials; // Use sharedMaterials to ensure proper swapping

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] == lightMaterial && !isGlass)
                {
                    materials[i] = glassMaterial; // Replace Light 2 with Glass 2
                    materialChanged = true;
                    Debug.Log($"Material swapped on {renderer.gameObject.name} at index {i} (Light 2 → Glass 2)");
                }
                else if (materials[i] == glassMaterial && isGlass)
                {
                    materials[i] = lightMaterial; // Replace Glass 2 back with Light 2
                    materialChanged = true;
                    Debug.Log($"Material swapped on {renderer.gameObject.name} at index {i} (Glass 2 → Light 2)");
                }
            }

            // Apply the updated materials back to the renderer
            if (materialChanged)
            {
                renderer.sharedMaterials = materials; // Assign modified materials back
            }
        }

        if (!materialChanged)
        {
            Debug.LogWarning("No materials were swapped. Please ensure both Light 2 and Glass 2 are correctly assigned.");
        }
    }
}
