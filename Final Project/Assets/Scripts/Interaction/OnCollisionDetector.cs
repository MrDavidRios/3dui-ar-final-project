using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Breaker"))
        {
            Breakable breakable = transform.GetComponentInChildren<Breakable>(false);
            
            if (breakable != null)
            {
                breakable.BreakObject(other.gameObject.transform.Find("Visuals/Root/Collider").gameObject,null);
            }
            else
            {
                Debug.Log("Breakable script not found");
            }
        }
        else
        {
            Breakable breakable = transform.GetComponentInChildren<Breakable>(false);
            if (breakable != null)
            {
                if (GetComponent<Rigidbody>().velocity.magnitude > 1.5f)
                    breakable.Break(null);
            }
        }
        
    }
}
