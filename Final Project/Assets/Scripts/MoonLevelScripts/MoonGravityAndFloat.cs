using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGravityAndFloat : MonoBehaviour
{
    private Rigidbody rb;

    // Floating parameters
    public float floatStrength = 1.62f; // Strength to counteract moon gravity
    public float randomRotationStrength = 0.2f; // Random rotation effect strength

    public float bounceAmplitude = 0.1f; 
    public float bounceFrequency = 1f;

    private float originalY; // To store the original y-position
    private float bounceOffset; // to prevent all Wall-Es from following same bounce pattern

    // Moon gravity
    private Vector3 moonGravity = new Vector3(0, -1.62f, 0);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false; // Turn off global gravity; currently set to 9.81 for other scenes too

        originalY = transform.position.y; // Store the initial y position
        bounceOffset = Random.Range(0f, 2f * Mathf.PI); // Random offset for each Wall-E
    }

    private void FixedUpdate()
    {
        ApplyMoonGravity();
        ApplyFloatStrength();
        ApplyRandomRotation();
        ApplyBouncing();
    }

    private void ApplyMoonGravity()
    {
        Vector3 gravityForce = moonGravity * rb.mass;
        rb.AddForce(gravityForce, ForceMode.Acceleration);
    }

    private void ApplyFloatStrength()
    {
        Vector3 floatForce = Vector3.up * floatStrength * rb.mass;
        rb.AddForce(floatForce, ForceMode.Acceleration);
    }

    private void ApplyRandomRotation()
    {
        transform.Rotate(Random.Range(-randomRotationStrength, randomRotationStrength),
                         Random.Range(-randomRotationStrength, randomRotationStrength),
                         Random.Range(-randomRotationStrength, randomRotationStrength));
    }

    private void ApplyBouncing()
    {
        // Calculate the new y position using a sine wave
        float newY = originalY + Mathf.Sin(Time.fixedTime * Mathf.PI * bounceFrequency + bounceOffset) * bounceAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
