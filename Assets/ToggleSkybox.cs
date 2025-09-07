using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class SkyboxToggle : MonoBehaviour
{
    public Material skybox1; // Default Skybox
    public Material skybox2; // Alternative Skybox
    public Toggle skyboxToggle; // Reference to the UI Toggle button

    void Start()
    {
        if (skyboxToggle != null)
        {
            skyboxToggle.onValueChanged.AddListener(ToggleSkybox);
        }
    }

    void ToggleSkybox(bool isOn)
    {
        RenderSettings.skybox = isOn ? skybox2 : skybox1;
 
    }
}
