using UnityEngine;
using UnityEngine.UI;

public class ToggleOppositeObjects : MonoBehaviour
{
    public GameObject onBackground; // "On Background" (Yellow Box)
    public GameObject directionalLight; // Directional Light
    public Toggle toggle; // Toggle Button

    void Start()
    {
        // Ensure the toggle starts in the correct state
        toggle.onValueChanged.AddListener(OnToggleChanged);
        
        // Set initial state manually
        OnToggleChanged(toggle.isOn);
    }

    void OnToggleChanged(bool isActive)
    {
        // Show "On Background" when toggle is ON
        onBackground.SetActive(isActive);

        // Set directional light to the opposite state
        directionalLight.SetActive(!isActive);
    }
}
