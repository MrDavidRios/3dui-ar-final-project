using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject originalObject;
    [SerializeField] GameObject brokenObject;
    float break_threshold = 1.0f;

    BoxCollider bc;

    private void Awake()
    {
        originalObject.SetActive(true);
        brokenObject.SetActive(false);

        bc = GetComponent<BoxCollider>();
    }

    private void OnMouseDown()
    {
        Break();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Breaker"))
        {
            Breaker breaker = other.gameObject.GetComponent<Breaker>();
            if (breaker != null)
            {
                Vector3 velocity = breaker.Velocity;
                float speed = velocity.magnitude;
                if (speed > break_threshold)
                    Break();
            }
        }
    }


    private void Break()
    {
        originalObject.SetActive(false);
        brokenObject.SetActive(true);

        bc.enabled = false;
    }
}