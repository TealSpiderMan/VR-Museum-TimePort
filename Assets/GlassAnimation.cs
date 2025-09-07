using UnityEngine;

public class GlassAnimation : MonoBehaviour
{
    private CanvasGroup canvasGroup; // For fade effect
    private Vector3 originalPosition; // Store the original position

    void Start()
    {
        // Store the original position
        originalPosition = transform.localPosition;

        // Ensure there's a CanvasGroup for fade effect
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1f; // Ensure it's fully visible at start
    }

    public void HideGlass()
    {
        // Fade Out and Move Down
        LeanTween.alphaCanvas(canvasGroup, 0f, 0.5f);
        LeanTween.moveLocalY(gameObject, originalPosition.y - 100f, 0.5f)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() =>
            {
                canvasGroup.alpha = 0; // Ensure it stays hidden
                gameObject.SetActive(false); // Disable after animation completes
            });
    }

    public void ShowGlass()
    {
        gameObject.SetActive(true); // Enable first
        canvasGroup.alpha = 0f; // Ensure it's invisible before animation
        transform.localPosition = new Vector3(originalPosition.x, originalPosition.y - 100f, originalPosition.z); // Start from down position

        // Fade In and Move Up
        LeanTween.alphaCanvas(canvasGroup, 1f, 0.5f);
        LeanTween.moveLocalY(gameObject, originalPosition.y, 0.5f)
            .setEase(LeanTweenType.easeInOutQuad);
    }
}
