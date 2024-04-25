using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class Breakable_New : MonoBehaviour
{
    [SerializeField] private GameObject originalObject;
    [SerializeField] private GameObject brokenObject;
    [SerializeField] private float breakThreshold = 0.3f; // Minimum speed required to trigger break

    [SerializeField] public UnityEvent OnBreak;

    private Rigidbody rb; // Rigidbody attached to this GameObject

    private void Awake()
    {
        originalObject.SetActive(true);
        brokenObject.SetActive(false);
        
        rb = GetComponent<Rigidbody>();
        if (rb == null) {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // Prevent Rigidbody from affecting the object initially
        }
    }

    private void Update()
    {
        // Sync the position and rotation of the broken object with the original object
        if (!brokenObject.activeSelf) {
            SyncPositionAndRotation();
        }
    }

    private void SyncPositionAndRotation()
    {
        // Match the broken object's transform to the original object's transform
        brokenObject.transform.position = originalObject.transform.position;
        brokenObject.transform.rotation = originalObject.transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Breaker"))
        {
            float speed = collision.relativeVelocity.magnitude; // Use the relative velocity from the collision
            if (speed > breakThreshold)
            {
                Break();
            }
        }
    }

    private void OnMouseDown()
    {
        Break(); // Allow breaking on mouse click for testing or other interaction
    }

    private void Break()
    {
        OnBreak?.Invoke(); // Invoke any events tied to the break action
        originalObject.SetActive(false); // Disable the original object
        brokenObject.SetActive(true); // Enable the broken object at the exact current location and rotation

        if (rb != null)
        {
            rb.isKinematic = true; // Optionally disable physics post-break if no longer needed
        }
    }
}
