using UnityEngine;
using UnityEngine.UI; // Required for Slider component

public class TrainSizeController : MonoBehaviour
{
    public Slider sizeSlider; // Reference to the MinMaxSlider in your UI
    public GameObject trainObject; // Reference to the "Train Object" GameObject
    public float minSize = 0.1f; // Minimum scale value
    public float maxSize = 2.0f; // Maximum scale value

    private Vector3 initialPosition; // Store the initial world position
    private Vector3 initialLocalPosition; // Store the initial local position (if parented)
    private Transform parentTransform; // Store the parent transform (if any)

    void Start()
    {
        // Ensure references are set
        if (sizeSlider == null || trainObject == null)
        {
            Debug.LogError("Please assign the Slider and Train Object in the Inspector!");
            return;
        }

        // Store the initial world position and local position of the train object
        initialPosition = trainObject.transform.position;
        parentTransform = trainObject.transform.parent; // Check if it has a parent
        if (parentTransform != null)
        {
            initialLocalPosition = trainObject.transform.localPosition;
        }

        // Set initial slider value (optional, can be adjusted in Inspector)
        sizeSlider.value = 0.5f;

        // Add listener for slider value changes
        sizeSlider.onValueChanged.AddListener(UpdateTrainSize);
        
        // Initialize train size
        UpdateTrainSize(sizeSlider.value);
    }

    void UpdateTrainSize(float value)
    {
        // Map the slider value (0 to 1) to the desired size range (minSize to maxSize)
        float newSize = Mathf.Lerp(minSize, maxSize, value);
        
        // Store the current position before scaling (world space)
        Vector3 currentPosition = trainObject.transform.position;

        // Apply the new scale to the train object (uniform scaling)
        trainObject.transform.localScale = new Vector3(newSize, newSize, newSize);

        // Reset the position to the initial world position to prevent movement
        trainObject.transform.position = initialPosition;

        // If the object has a parent, ensure local position is maintained
        if (parentTransform != null)
        {
            trainObject.transform.localPosition = initialLocalPosition;
        }
    }

    void OnDestroy()
    {
        // Remove listener to prevent memory leaks
        if (sizeSlider != null)
        {
            sizeSlider.onValueChanged.RemoveListener(UpdateTrainSize);
        }
    }
}