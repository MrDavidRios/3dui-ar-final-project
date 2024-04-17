using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGravity : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moonGravity = new Vector3(0, -1.62f, 0);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; // Turn off global gravity (set to -9.81)
        }


        // apply moon gravity settings to each rigidbody in the children
        foreach(Transform child in transform)
        {
            Rigidbody child_rb = child.GetComponent<Rigidbody>();
            if (child_rb != null)
            {
                child_rb.useGravity = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // apply to parent

        if (rb != null)
        {
            Vector3 gravity = moonGravity * rb.mass;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }

        // apply moon gravity settings to each rigidbody in the children
        foreach (Transform child in transform)
        {
            Rigidbody child_rb = child.GetComponent<Rigidbody>();
            if (child_rb != null)
            {
                Vector3 child_gravity = moonGravity * child_rb.mass;
                child_rb.AddForce(child_gravity, ForceMode.Acceleration);
            }
        }
    }
}
