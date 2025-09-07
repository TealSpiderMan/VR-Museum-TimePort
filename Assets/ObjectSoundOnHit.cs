using UnityEngine;

public class ObjectSoundOnHit : MonoBehaviour
{
    public AudioClip hitSound;  // Assign your sound in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object collides with the floor
        if (collision.gameObject.CompareTag("Floor")) 
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
