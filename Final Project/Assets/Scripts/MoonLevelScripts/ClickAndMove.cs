using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ClickAndMove : MonoBehaviour
{
    public UnityEvent onHitEvent;

    public GameObject eveObject; 
    public AudioClip hitSound;
    private bool hasMoved = false;
    private float goldDuration = 2f;
    private float eveDistanceFromPlayer = 150f; 
    private float eveDistance = 120f; // distance in front of the player (a bit off but okay)
    private void OnTriggerEnter(Collider other)
    {
        if (!hasMoved && other.CompareTag("Breaker"))
        {
            // sound!
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }

            
            StartCoroutine(MoveUpward(transform, 0.5f, goldDuration));

            
            if (eveObject != null)
            {
                // x-z only!
                Vector3 forwardXZ = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
                Vector3 evePosition = transform.position + forwardXZ * eveDistance;

                eveObject.transform.position = evePosition;
                eveObject.transform.LookAt(new Vector3(transform.position.x, eveObject.transform.position.y, transform.position.z));

                
                Vector3 newRotation = eveObject.transform.eulerAngles;
                newRotation.x = 16f;
                eveObject.transform.eulerAngles = newRotation;
            }


            hasMoved = true;

            
            onHitEvent.Invoke();
        }
    }

    IEnumerator MoveUpward(Transform objectTransform, float distance, float moveDuration)
    {
        Vector3 start = objectTransform.position;
        Vector3 end = new Vector3(start.x, start.y + distance, start.z);
        float elapsed = 0;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            objectTransform.position = Vector3.Lerp(start, end, elapsed / moveDuration);
            yield return null;
        }

        objectTransform.position = end;
    }
}
