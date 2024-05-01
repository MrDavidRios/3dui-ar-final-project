using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatStrength : MonoBehaviour
{
    public float floatStrength = 1.62f;  // Strength to counteract moon gravity
    public float randomRotationStrength = 0.2f;  // Random rotation effect strength

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();  // Ensure there's a Rigidbody component
        }
    }

    void Update()
    {
        rb.AddForce(Vector3.up * floatStrength * rb.mass); // apply upwards force

        transform.Rotate(Random.Range(-randomRotationStrength, randomRotationStrength),
                         Random.Range(-randomRotationStrength, randomRotationStrength),
                         Random.Range(-randomRotationStrength, randomRotationStrength));
    }
}
