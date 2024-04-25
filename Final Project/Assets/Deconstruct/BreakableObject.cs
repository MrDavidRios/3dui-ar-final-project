using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private float breakThreshold = 1.0f;  // Force required to break
    [SerializeField] private float moveThreshold = 0.5f;   // Force required to move without breaking
    [SerializeField] private float impactRadius = 5.0f;    // Radius for breaking effect
    [SerializeField] private float forceMultiplier = 10.0f; // Multiplies the force applied to pieces for breaking
    [SerializeField] public UnityEvent OnBreak;

    private GameObject[] pieces;  // Array of pieces
    private Rigidbody rb;        // Rigidbody for the whole object for non-breaking interactions

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true; // Initially not affected by physics

        InitializePieces();
    }

    private void InitializePieces()
    {
        // Automatically assign all children as pieces
        int childCount = transform.childCount;
        pieces = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            pieces[i] = transform.GetChild(i).gameObject;
            Rigidbody pieceRb = pieces[i].GetComponent<Rigidbody>();
            if (pieceRb == null)
            {
                pieceRb = pieces[i].AddComponent<Rigidbody>();
            }
            pieceRb.isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float collisionForceMagnitude = collision.impulse.magnitude;

        if (collisionForceMagnitude >= breakThreshold)
        {
            ApplyImpact(collision.contacts[0].point, collision.impulse);
        }
        else if (collisionForceMagnitude >= moveThreshold)
        {
            rb.isKinematic = false;
            rb.AddForce(collision.impulse * forceMultiplier, ForceMode.Impulse);
        }
    }

    public void ApplyImpact(Vector3 impactPoint, Vector3 impactForce)
    {
        foreach (var piece in pieces)
        {
            Rigidbody pieceRb = piece.GetComponent<Rigidbody>();
            if (Vector3.Distance(piece.transform.position, impactPoint) < impactRadius)
            {
                pieceRb.isKinematic = false;
                pieceRb.AddForce(impactForce * forceMultiplier, ForceMode.Impulse);
            }
        }
        gameObject.SetActive(false);  // Optionally hide the parent object
    }
}
