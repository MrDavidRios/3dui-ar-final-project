using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SmashOnChildHit : MonoBehaviour
{
    [SerializeField] private UnityEvent OnSmash; // Event triggered when a child object is smashed
    [SerializeField] private float smashVelocityThreshold = 1.0f; // Minimum collision velocity to trigger smash

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collider has the "Breaker" tag and the velocity is above the threshold
        if (collision.collider.CompareTag("Breaker") && collision.relativeVelocity.magnitude > smashVelocityThreshold)
        {
            Rigidbody[] childRigidbodies = GetComponentsInChildren<Rigidbody>();

            // Enable physics for all child rigidbodies
            foreach (Rigidbody rb in childRigidbodies)
            {
                if (rb.isKinematic) // Check if the Rigidbody is kinematic
                {
                    rb.isKinematic = false; // Make it dynamic
                    rb.transform.SetParent(null); // Optionally detach from the parent
                }
            }

            // Trigger event for additional actions
            OnSmash.Invoke();
        }
    }
}
