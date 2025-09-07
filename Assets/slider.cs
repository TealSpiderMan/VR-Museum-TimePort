using UnityEngine;
using UnityEngine.UI;

public class ChargingSound : MonoBehaviour
{
    public Slider chargeSlider;
    public AudioSource chargeAudio;
    
    public float minPitch = 0.5f;  // Low pitch
    public float maxPitch = 2.0f;  // High pitch
    public float minVolume = 0.1f;
    public float maxVolume = 1.0f;

    void Start()
    {
        chargeAudio.loop = true;  // Loop sound for continuous charging effect
        chargeAudio.Play();  // Start the sound but mute it initially
        chargeAudio.volume = 0;
    }

    void Update()
{
    float chargeValue = chargeSlider.value;

    // Adjust pitch and volume based on slider value
    chargeAudio.pitch = Mathf.Lerp(minPitch, maxPitch, chargeValue);
    chargeAudio.volume = Mathf.Lerp(minVolume, maxVolume, chargeValue);

    // Play sound only if charging is active
    if (chargeValue > 0 && !chargeAudio.isPlaying)
    {
        chargeAudio.Play();
    }
    else if (chargeValue == 0 && chargeAudio.isPlaying)
    {
        chargeAudio.Stop();
    }
}

}
