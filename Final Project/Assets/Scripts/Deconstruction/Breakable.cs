using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;


[SelectionBase]
public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject originalObject;
    [SerializeField] GameObject brokenObject;
    float break_threshold = 0.4f;
    float impactRadius = 0.3f; // Radius around the hit point to affect pieces
    [SerializeField] public UnityEvent OnBreak;

    BoxCollider bc;
    private Rigidbody rb;
    private List<Rigidbody> childRigidbodies;

    private enum BreakType
    {
        Replace,
        Detach
    }

    [SerializeField] private BreakType breakType = BreakType.Replace;

    private void Awake()
    {
        bc = GetComponent<BoxCollider>();
        switch (breakType)
        {
            case BreakType.Detach:
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

                break;
            case BreakType.Replace:
                originalObject.SetActive(true);
                brokenObject.SetActive(false);
                break;
        }

    }

    private void OnMouseDown()
    {
        Break(null);
    }

    public void BreakObject(GameObject breakerObj, [CanBeNull] Collider collider)
    {
        Breaker breaker = breakerObj.GetComponent<Breaker>();
        if (breaker != null)
        {
            Vector3 velocity = breaker.Velocity;
            float speed = velocity.magnitude;
            if (speed > break_threshold)
            {
                breaker.HitObject();
                Break(collider);
            }
            else
            {
                Debug.Log("Speed not enough to break " + speed);
            }
        }
        else
        {
            Debug.Log("Breaker script not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Breaker"))
        {
            BreakObject(other.gameObject, other);
        }
    }

    public void Break([CanBeNull] Collider collider)
    {
        OnBreak?.Invoke();
        switch (breakType)
        {
            case BreakType.Replace:
                originalObject.SetActive(false);
                brokenObject.SetActive(true);
                bc.enabled = false;
                break;
            case BreakType.Detach:
                if (collider == null)
                {
                    Debug.LogError("No collider found");
                    break;
                }

                Vector3 impactPoint = collider.ClosestPoint(transform.position);
                HandleImpact(impactPoint, collider.GetComponent<Breaker>().Velocity, collider);
                break;
            default:
                throw new ArgumentOutOfRangeException();
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