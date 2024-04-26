using UnityEngine;
using System.Collections.Generic;

public class LocalizedBreakableObject : MonoBehaviour
{
    public float impactRadius = 0.5f; // Radius around the hit point to affect pieces
    public float forceThreshold = 0.3f; // Minimum force to detach pieces
    public float moveThreshold = 0.1f; // Force that the object can sustain without breaking

    private Rigidbody rb; // Rigidbody for the whole object
    private List<Rigidbody> childRigidbodies; // List of Rigidbodies for child pieces

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        childRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        foreach (var childRb in childRigidbodies)
        {
            childRb.isKinematic = true;
            childRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Breaker"))
        {
            Vector3 impactPoint = collision.contacts[0].point;
            Vector3 impactForce = collision.impulse;
            float forceMagnitude = impactForce.magnitude;

            if (forceMagnitude > forceThreshold)
            {
                HandleImpact(impactPoint, impactForce, collision.collider);
            }
            else if (forceMagnitude > moveThreshold)
            {
                rb.AddForce(impactForce, ForceMode.Impulse);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Breaker"))
        {
            // Vector3 impactPoint = collision.contacts[0].point;
            Vector3 impactPoint = other.ClosestPoint(transform.position);
            // Vector3 impactForce = other.impulse;
            Vector3 impactForce = other.GetComponent<Breaker>().Velocity;
            float forceMagnitude = impactForce.magnitude;

            if (forceMagnitude > forceThreshold)
            {
                HandleImpact(impactPoint, impactForce, other);
            }
            else if (forceMagnitude > moveThreshold)
            {
                rb.AddForce(impactForce, ForceMode.Impulse);
            }
        }
    }

    void HandleImpact(Vector3 impactPoint, Vector3 impactForce, Collider otherCollider)
    {
        HashSet<Rigidbody> affectedRigidbodies = new HashSet<Rigidbody>();
        foreach (var childRb in childRigidbodies)
        {
            if (Vector3.Distance(childRb.position, impactPoint) <= impactRadius)
            {
                affectedRigidbodies.Add(childRb);
            }
        }

        // Check adjacency based on actual touching colliders, not just distance
        HashSet<Rigidbody> touchingRigidbodies = new HashSet<Rigidbody>();
        foreach (var rb in affectedRigidbodies)
        {
            Collider[] rbColliders = rb.GetComponents<Collider>();
            foreach (var rbCollider in rbColliders)
            {
                if (rbCollider.bounds.Intersects(otherCollider.bounds))
                {
                    touchingRigidbodies.Add(rb);
                    break;
                }
            }
        }

        // Detach the pieces that are physically touching the bat collider
        foreach (var rb in touchingRigidbodies)
        {
            DetachPiece(rb, impactForce);
        }
    }

    void DetachPiece(Rigidbody pieceRb, Vector3 force)
    {
        pieceRb.transform.SetParent(null); // Detach from the parent object
        pieceRb.isKinematic = false; // Allow it to be affected by physics
        pieceRb.AddForce(force, ForceMode.Impulse);
    }
}