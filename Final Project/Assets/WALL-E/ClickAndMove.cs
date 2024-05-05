using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ClickAndMove : MonoBehaviour
{
    public UnityEvent onHitEvent; 

    public GameObject targetPrefab; 
    private bool hasMoved = false;
    private float goldDuration = 2f; 
    private float targetDuration = 6f; 

    private void OnTriggerEnter(Collider other)
    {
        if (!hasMoved && other.CompareTag("Breaker"))
        {
           
            StartCoroutine(MoveUpward(transform, 0.5f, goldDuration));
            if (targetPrefab != null)
            {
               
                StartCoroutine(MoveTargetUpward(targetPrefab.transform, targetDuration));
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

    IEnumerator MoveTargetUpward(Transform targetTransform, float duration)
    {
        Vector3 startPosition = targetTransform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + 200f, startPosition.z);
        float elapsed = 0f; 

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            targetTransform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            yield return null;
        }

        targetTransform.position = endPosition; 
    }
}
