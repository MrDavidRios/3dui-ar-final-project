using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashOnClick : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    StartCoroutine(DetachAndReleaseChildren());
                }
            }
        }
    }

    IEnumerator DetachAndReleaseChildren()
    {
        // Detach from parent and make this object dynamic
        transform.parent = null;
        MakeDynamic(GetComponent<Rigidbody>());

        // Wait a frame before detaching children to avoid force accumulation
        yield return null;

        // Detach and make all children dynamic
        int children = transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            child.parent = null;
            MakeDynamic(child.GetComponent<Rigidbody>());
        }
    }

    void MakeDynamic(Rigidbody rb)
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
