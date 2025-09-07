using UnityEngine;

public class WallMover : MonoBehaviour
{
    public float moveDistance = 5f; // How far the wall moves up
    public float moveSpeed = 2f; // Speed of the movement
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMovingUp = false; // Track movement direction
    private bool isAnimating = false; // Prevent multiple triggers

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, moveDistance, 0);
        gameObject.SetActive(false); // Initially hidden
    }

    public void ToggleWall()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                StartCoroutine(MoveWall(targetPosition, true));
            }
            else
            {
                StartCoroutine(MoveWall(startPosition, false));
            }
        }
    }

    private System.Collections.IEnumerator MoveWall(Vector3 destination, bool movingUp)
    {
        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (!movingUp)
        {
            gameObject.SetActive(false); // Hide after moving down
        }

        isMovingUp = movingUp;
        isAnimating = false; // Allow toggling again
    }
}
