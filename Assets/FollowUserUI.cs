using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform vrHead; // Assign the Main Camera (player's head)
    
    [Header("UI Offset Settings")]
    public float offsetRight = 0.3f;  // Move UI right
    public float offsetUp = 0f;       // Move UI up/down
    public float offsetForward = 0.2f; // Move UI forward

    void Update()
    {
        if (vrHead != null)
        {
            // Calculate direction to the player
            Vector3 direction = transform.position - vrHead.position;
            transform.rotation = Quaternion.LookRotation(direction);

            // Move the UI based on inspector values
            Vector3 newPos = transform.parent.position 
                             + transform.parent.right * offsetRight  // Move right
                             + transform.parent.up * offsetUp        // Move up/down
                             + transform.parent.forward * offsetForward; // Move forward/backward

            transform.position = newPos;
        }
    }
}
