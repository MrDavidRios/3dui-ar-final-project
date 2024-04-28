using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SmashOnClick_Bat : MonoBehaviour
{
    [SerializeField] private UnityEvent OnSmash; // Event triggered when object is smashed
    [SerializeField] private float smashVelocityThreshold = 1.0f; // Minimum collision velocity to trigger smash

    private List<Rigidbody> allRigidbodies; // List to store all descendant Rigidbodies

    void Awake()
    {
        // Gather all descendant rigidbodies and initially set them to kinematic
        allRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>(true));
        foreach (var rb in allRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collider has the "Breaker" tag and the velocity is above the threshold
        if (collision.collider.CompareTag("Breaker") && collision.relativeVelocity.magnitude > smashVelocityThreshold)
        {
            StartCoroutine(DetachAndReleaseChildren(collision.impulse));
            OnSmash?.Invoke(); // Trigger any additional events
        }
    }

    IEnumerator DetachAndReleaseChildren(Vector3 impactForce)
    {
        // Wait a frame before detaching children to avoid force accumulation
        yield return null;

        // Randomly decide how many rigidbodies to detach
        int countToDetach = Random.Range(1, allRigidbodies.Count / 2);

        for (int i = 0; i < countToDetach; i++)
        {
            int index = Random.Range(0, allRigidbodies.Count);
            Rigidbody rb = allRigidbodies[index];
            if (rb != null)
            {
                // Detach from parent
                rb.transform.SetParent(null);

                // Enable physics interaction
                rb.isKinematic = false;

                // Apply the impact force to simulate physical interaction
                rb.AddForce(impactForce, ForceMode.Impulse);
            }
        }

        // Apply a smaller force to the remaining structure to simulate tilting or other physical effects
        foreach (var rb in allRigidbodies)
        {
            if (rb.isKinematic)  // Still attached pieces
            {
                rb.isKinematic = false; // Allow physics but with less force
                rb.AddForce(impactForce * 0.1f, ForceMode.Impulse); // Apply a reduced force
            }
        }
    }
}
