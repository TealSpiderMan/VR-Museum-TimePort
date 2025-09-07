using System.Collections;
using UnityEngine;

public class ATMCashSpawner : MonoBehaviour
{
    public GameObject cashPrefab; // Assign the cash prefab in the Inspector
    public Transform spawnPoint; // Spawn location of cash
    public int cashAmount = 5; // Number of cash notes to dispense
    public float spawnDelay = 0.2f; // Delay between each cash note
    public float ejectForce = 2f; // Force applied to eject the cash

    private bool isDispensing = false; // Prevent multiple activations

    public void StartCashDispensing()
    {
        if (!isDispensing)
        {
            isDispensing = true;
            StartCoroutine(DispenseCash());
        }
    }

    IEnumerator DispenseCash()
    {
        for (int i = 0; i < cashAmount; i++)
        {
            GameObject cash = Instantiate(cashPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = cash.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 randomForce = new Vector3(
                    Random.Range(-0.5f, 0.5f), // Slight left-right variation
                    1f, // Upward force
                    Random.Range(0.5f, 1f) // Forward force
                ) * ejectForce;

                rb.AddForce(randomForce, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * ejectForce, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(spawnDelay);
        }

        isDispensing = false; // Reset flag so the button can be pressed again
    }
}
