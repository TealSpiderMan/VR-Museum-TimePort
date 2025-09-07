using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{
    public Toggle toggle; // Assign the Toggle UI
    public AudioSource audioSource; // Assign the AudioSource

    void Start()
    {
        toggle.onValueChanged.AddListener(PlayOrStopSound);
    }

    void PlayOrStopSound(bool isOn)
    {
        if (isOn)
        {
            audioSource.Play(); // Play when toggled ON
        }
        else
        {
            audioSource.Stop(); // Stop when toggled OFF
        }
    }
}
