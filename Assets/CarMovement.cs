using UnityEngine;

public class CarLoop : MonoBehaviour
{
    public float speed = 5f;
    private float timer = 0f;
    private int stage = 0;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        // Store the starting position and rotation
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (stage == 0 && timer < 5f)
        {
            // Move forward for 5 seconds
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (stage == 0)
        {
            // Rotate left to -270 degrees
            transform.rotation = Quaternion.Euler(0, -270, 0);
            stage = 1;
            timer = 0f;
        }
        else if (stage == 1 && timer < 7f)
        {
            // Move forward for another 5 seconds
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (stage == 1)
        {
            // Rotate right to 0 degrees
            transform.rotation = Quaternion.Euler(0, 0, 0);
            stage = 2;
        }
        else
        {
            // Continue moving straight
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            // Reset the car to its original position and rotation
            transform.position = startPosition;
            transform.rotation = startRotation;
            timer = 0f;
            stage = 0;
        }
    }
}
