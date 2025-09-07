using UnityEngine;
using UnityEngine.UI;

public class ToggleColorController : MonoBehaviour
{
    public Toggle toggle;
    public Image targetImage; // The image that should change color
    public Color onColor = new Color(0.75f, 0.2f, 0.75f); // Custom purple
    public Color offColor = Color.white;

    void Start()
    {
        toggle.onValueChanged.AddListener(UpdateColor);
        UpdateColor(toggle.isOn); // Ensure it updates initially
    }

    void UpdateColor(bool isOn)
    {
        if (targetImage != null)
        {
            targetImage.color = isOn ? onColor : offColor;
        }
    }
}
