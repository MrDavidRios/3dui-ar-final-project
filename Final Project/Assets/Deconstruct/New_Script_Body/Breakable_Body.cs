using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class Breakable_Body : MonoBehaviour
{
    [SerializeField] GameObject originalObject;
    [SerializeField] GameObject brokenObject;
    [SerializeField] GameObject originalParent; // Assignable slot for the original parent
    [SerializeField] GameObject grandParent; // Assignable slot for the grand parent
    float break_threshold = 0.05f;
    float impactRadius = 0.01f;
    [SerializeField] public UnityEvent OnBreak;

    BoxCollider bc;
    private Rigidbody rb;
    private List<Rigidbody> childRigidbodies;

    [SerializeField] float floatingForce = 2.0f; // Default floating force
    [SerializeField] bool useCustomGravity = true; // Use custom gravity settings
    [SerializeField] string bodyTag = "Body";
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

    public void BreakObject(GameObject breakerObj, Collider collider)
    {
        Breaker breaker = breakerObj.GetComponent<Breaker>();
        if (breaker != null && breaker.Velocity.magnitude > break_threshold)
        {
            breaker.HitObject();
            Break(collider);
        }
        else
        {
            Debug.Log("Speed not enough to break " + breaker.Velocity.magnitude);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Breaker"))
        {
            BreakObject(other.gameObject, other);
        }
    }

    public void Break(Collider collider)
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
                    return;
                }
                Vector3 impactPoint = collider.ClosestPoint(transform.position);
                HandleImpact(impactPoint, collider.GetComponent<Breaker>().Velocity, collider);
                GroupUnaffectedSiblings();
                DetachAllSiblings();
                DetachGrandChildren(); // Fixed: No argument passed
                ApplyFloatingAndRotationToParent();
                break;
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
                MakeFloatAndRotate(childRb); // Apply floating and rotation effect
            }
        }

        foreach (var rb in affectedRigidbodies)
        {
            DetachPiece(rb, impactForce);

            // Check if the impacted object has the "Body" tag
            // if (otherCollider.CompareTag("Body"))
            // {
            //     DetachAllSiblings();
            //     DetachGrandChildren();
            // }
        }
    }


    void DetachPiece(Rigidbody pieceRb, Vector3 force)
    {
        pieceRb.transform.SetParent(null);
        pieceRb.isKinematic = false; // Ensure isKinematic is false
        pieceRb.useGravity = true; // Enable gravity if necessary
        pieceRb.detectCollisions = true; // Enable collision detection
        pieceRb.AddForce(force, ForceMode.Impulse);
        MakeFloatAndRotate(pieceRb); // Apply floating and rotation effect

        SetParentsNonKinematic(pieceRb.transform); // Set all parent objects to non-kinematic

        if (pieceRb.CompareTag(bodyTag)) // Check if the collided object has the assigned body tag
        {
            DetachGrandChildren(); // Detach grandchildren regardless of the collision point
            DetachAllSiblings();
        }
    }



    void GroupUnaffectedSiblings()
    {
        GameObject newParent = new GameObject("NewGroup");
        Rigidbody newParentRb = newParent.AddComponent<Rigidbody>();
        newParentRb.isKinematic = false;

        foreach (Transform sibling in originalParent.transform)
        {
            Breakable_Body siblingBreakable = sibling.GetComponent<Breakable_Body>();
            if (siblingBreakable != null && !childRigidbodies.Contains(siblingBreakable.rb))
            {
                sibling.SetParent(newParent.transform);
                Rigidbody childRb = sibling.GetComponent<Rigidbody>();
                if (childRb != null)
                {
                    childRb.isKinematic = false;
                    MakeFloatAndRotate(childRb); // Apply floating and rotation effect
                }
            }
        }
    }

    void DetachAllSiblings()
    {
        foreach (Transform sibling in originalParent.transform)
        {
            Rigidbody siblingRb = sibling.GetComponent<Rigidbody>();
            if (siblingRb != null)
            {
                sibling.SetParent(null);
                siblingRb.isKinematic = false;
                MakeFloatAndRotate(siblingRb); // Apply floating and rotation effect

                SetParentsNonKinematic(sibling);
            }
        }
    }

    void DetachGrandChildren()
    {
        if (grandParent == null)
        {
            Debug.LogError("Grand parent not assigned");
            return;
        }

        foreach (Transform child in grandParent.transform)
        {
            Rigidbody childRb = child.GetComponent<Rigidbody>();
            if (childRb != null)
            {
                child.SetParent(null); // Detach from the grand parent
                childRb.isKinematic = false; // Set to non-kinematic
                MakeFloatAndRotate(childRb); // Apply floating and rotation effect

                SetParentsNonKinematic(child); // Set all parent objects to non-kinematic
            }
        }
    }



    void SetParentsNonKinematic(Transform child)
    {
        Transform parent = child.parent;
        while (parent != null)
        {
            Rigidbody parentRb = parent.GetComponent<Rigidbody>();
            if (parentRb != null)
            {
                parentRb.isKinematic = false;
                parent = parent.parent;
            }
            else
            {
                break;
            }
        }
    }

    void MakeFloatAndRotate(Rigidbody rb)
    {
        rb.useGravity = !useCustomGravity;

        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = Mathf.Abs(randomDirection.y);
        Vector3 floatForce = randomDirection * floatingForce;

        Vector3 randomTorque = Random.insideUnitSphere * floatingForce;

        rb.AddForce(floatForce, ForceMode.Acceleration);
        rb.AddTorque(randomTorque, ForceMode.Acceleration);
    }

    void ApplyFloatingAndRotationToParent()
    {
        if (grandParent == null)
        {
            return;
        }

        Rigidbody grandParentRb = grandParent.GetComponent<Rigidbody>();
        if (grandParentRb == null)
        {
            grandParentRb = grandParent.AddComponent<Rigidbody>();
        }

        grandParentRb.useGravity = !useCustomGravity;

        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = Mathf.Abs(randomDirection.y);
        Vector3 floatForce = randomDirection * floatingForce;

        Vector3 randomTorque = Random.insideUnitSphere * floatingForce;

        grandParentRb.AddForce(floatForce, ForceMode.Acceleration);
        grandParentRb.AddTorque(randomTorque, ForceMode.Acceleration);
    }
}