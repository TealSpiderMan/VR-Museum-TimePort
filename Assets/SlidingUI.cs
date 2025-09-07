using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlidingUIController : MonoBehaviour
{
    public GameObject targetUI;  // The UI element to move
    public float targetY = 5.9f; // Target Y position
    public float moveSpeed = 2f; // Speed of movement
    private bool isMoving = false;

    public void SlideUp()
    {
        if (targetUI != null && !isMoving)
        {
            StartCoroutine(SmoothMove(targetUI.transform));
        }
    }

    private IEnumerator SmoothMove(Transform uiTransform)
    {
        isMoving = true;
        Vector3 startPosition = uiTransform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);

        float elapsedTime = 0f;
        float duration = Mathf.Abs(targetY - startPosition.y) / moveSpeed;

        while (elapsedTime < duration)
        {
            uiTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiTransform.position = targetPosition;
        isMoving = false;
    }
}
