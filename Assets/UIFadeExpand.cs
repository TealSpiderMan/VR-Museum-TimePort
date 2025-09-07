using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro

public class UIFade : MonoBehaviour
{
    public float animationTime = 0.5f; // Duration of fade
    private Image[] images;
    private TextMeshProUGUI[] textElements;
    private bool isVisible = false; // UI starts hidden

    private void Awake()
    {
        // Get all Images and TMP Text components in children
        images = GetComponentsInChildren<Image>();
        textElements = GetComponentsInChildren<TextMeshProUGUI>();

        SetAlpha(0f); // Start fully transparent
    }

    public void ToggleUI()
    {
        isVisible = !isVisible;

        if (isVisible)
        {
            // Fade in
            LeanTween.value(gameObject, 0f, 1f, animationTime).setOnUpdate(SetAlpha);
        }
        else
        {
            // Fade out
            LeanTween.value(gameObject, 1f, 0f, animationTime).setOnUpdate(SetAlpha);
        }
    }

    private void SetAlpha(float alpha)
    {
        // Update alpha for all images
        foreach (var img in images)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }

        // Update alpha for all TMP text elements
        foreach (var text in textElements)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}
