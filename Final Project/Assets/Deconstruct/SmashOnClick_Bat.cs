using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SmashOnClick_Bat : MonoBehaviour
{
    [SerializeField] private UnityEvent OnSmash; // Event triggered when object is smashed
    [SerializeField] private float smashVelocityThreshold = 1.0f; // Minimum collision velocity to trigger smash

    private Rigidbody[] childRigidbodies; // Array to store children's Rigidbodies

    void Awake()
    {
        // Gather all child rigidbodies and initially set them to kinematic
        childRigidbodies = GetComponentsInChildren<Rigidbody>(true);
        foreach (var rb in childRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collider has the "Breaker" tag and the velocity is above the threshold
        if (collision.collider.CompareTag("Breaker") && collision.relativeVelocity.magnitude > smashVelocityThreshold)
        {
            StartCoroutine(DetachAndReleaseChildren());
            OnSmash?.Invoke(); // Trigger any additional events
        }
    }

    IEnumerator DetachAndReleaseChildren()
    {
        // Wait a frame before detaching children to avoid force accumulation
        yield return null;

        foreach (var rb in childRigidbodies)
        {
            rb.transform.parent = null; // Detach from parent
            rb.isKinematic = false;     // Enable physics interaction
            rb.velocity = Vector3.zero; // Reset velocities
            rb.angularVelocity = Vector3.zero;
        }
    }
}
