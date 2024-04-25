using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[SelectionBase]
public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject originalObject;
    [SerializeField] GameObject brokenObject;
    float break_threshold = 0.4f;

    [SerializeField] public UnityEvent OnBreak;

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

    public void BreakObject(GameObject breakerObj)
    {
        Breaker breaker = breakerObj.GetComponent<Breaker>();
        if (breaker != null)
        {
            Vector3 velocity = breaker.Velocity;
            float speed = velocity.magnitude;
            if (speed > break_threshold)
            {
                breaker.HitObject();
                Break();
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
            BreakObject(other.gameObject);
        }
    }


    public void Break()
    {
        OnBreak?.Invoke();
        originalObject.SetActive(false);
        brokenObject.SetActive(true);
        bc.enabled = false;
    }
}