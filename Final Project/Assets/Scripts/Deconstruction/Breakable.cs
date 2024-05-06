using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject originalObject;
    [SerializeField] GameObject brokenObject;
    [SerializeField] GameObject originalParent; 
    [SerializeField] GameObject grandParent;
    float break_threshold = 0.15f;
    float impactRadius = 0.5f;
    [SerializeField] public UnityEvent OnBreak;

    BoxCollider bc;
    private Rigidbody rb;
    private List<Rigidbody> childRigidbodies;

    [SerializeField] float floatingForce = 2.0f; // default floating force
    [SerializeField] bool useCustomGravity = true; 
    [SerializeField] string bodyTag = "Body";

    [SerializeField] private AudioSource breakSound; // for Wall-E only (detach type)

 // NEW:
    private bool isBreakable = true; // Used in other functions to stop breaking after objective completed
    private bool isBroken = false;


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
        if (!isBreakable || isBroken) return; // new here as well

        OnBreak?.Invoke();
        isBroken = true;

        switch (breakType)
        {
            case BreakType.Replace:
                originalObject.SetActive(false);
                brokenObject.SetActive(true);
                StartCoroutine(DeactivateBrokenObjectAfterDelay(5f)); // no more shakinggg
                bc.enabled = false;
                break;
            case BreakType.Detach:

                if (breakSound != null)
                    breakSound.Play();
                if (collider == null)
                {
                    Debug.LogError("No collider found");
                    return;
                }
                Vector3 impactPoint = collider.ClosestPoint(transform.position);
                HandleImpact(impactPoint, collider.GetComponent<Breaker>().Velocity, collider);
                GroupUnaffectedSiblings();
                DetachAllSiblings();
                //  DetachGrandChildren();// Fixed: No argument passed // keep this note don't delete
                ApplyFloatingAndRotationToParent();
                
                break;
        }
    }

    // delete the broken object
    IEnumerator DeactivateBrokenObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        brokenObject.SetActive(false);
    }

    // NEW:
    public void MakeUnbreakable()
    {
        isBreakable = false;
    }

    // for inactive after 7 sec
    void ScheduleDeactivation(GameObject obj)
    {
        StopCoroutine("DeactivateAfterDelay"); 
        StartCoroutine(DeactivateAfterDelay(obj, 7f));
    }

    IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }


    void HandleImpact(Vector3 impactPoint, Vector3 impactForce, Collider otherCollider)
    {
        HashSet<Rigidbody> affectedRigidbodies = new HashSet<Rigidbody>();
        foreach (var childRb in childRigidbodies)
        {
            if (Vector3.Distance(childRb.position, impactPoint) <= impactRadius)
            {
                affectedRigidbodies.Add(childRb);
                MakeFloatAndRotate(childRb); 
            }
        }

        foreach (var rb in affectedRigidbodies)
        {
            DetachPiece(rb, impactForce);

            // remember the "Body" tag
            if (rb.CompareTag("Body"))
            {
                DetachAllSiblings();
                DetachGrandChildren();
            }
        }
    }


    void DetachPiece(Rigidbody pieceRb, Vector3 force)
    {
        pieceRb.transform.SetParent(null);
        pieceRb.isKinematic = false;
        pieceRb.useGravity = true;
        pieceRb.detectCollisions = true;
        pieceRb.AddForce(force, ForceMode.Impulse);
        MakeFloatAndRotate(pieceRb);

        SetParentsNonKinematic(pieceRb.transform);

        // deactivate
        ScheduleDeactivation(pieceRb.gameObject);

        if (pieceRb.CompareTag(bodyTag))
        {
            Debug.Log("Detaching the body's siblings and grandchildren");
            DetachAllSiblings();
            DetachGrandChildren();
        }
    }




    void GroupUnaffectedSiblings()
    {
        GameObject newParent = new GameObject("NewGroup");
        Rigidbody newParentRb = newParent.AddComponent<Rigidbody>();
        newParentRb.isKinematic = false;

        foreach (Transform sibling in originalParent.transform)
        {
            if (!childRigidbodies.Contains(sibling.GetComponent<Rigidbody>()))
            {
                sibling.SetParent(newParent.transform);
                Rigidbody childRb = sibling.GetComponent<Rigidbody>();
                if (childRb != null)
                {
                    childRb.isKinematic = false;
                    MakeFloatAndRotate(childRb);
                     // deactivate
                    ScheduleDeactivation(sibling.gameObject);
                }
            }
        }

         // deactivate
        ScheduleDeactivation(newParent);
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
                MakeFloatAndRotate(siblingRb);

                 // deactivate
                ScheduleDeactivation(sibling.gameObject);
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
                child.SetParent(null);
                childRb.isKinematic = false;
                MakeFloatAndRotate(childRb);

                 // deactivate
                ScheduleDeactivation(child.gameObject);
            }
        }

         // deactivate
        ScheduleDeactivation(grandParent);
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