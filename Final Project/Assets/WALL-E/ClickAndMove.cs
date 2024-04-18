using UnityEngine;
using System.Collections;

public class ClickAndMove : MonoBehaviour
{
    public GameObject targetPrefab; // Assign this in the Inspector
    private bool hasMoved = false; // Flag to ensure movement happens only once
    private float goldDuration = 2f; // Duration for the gold prefab's movement
    private float targetDuration = 6f; // Duration for the target prefab's movement

    private void OnMouseDown()
    {
        if (!hasMoved)
        {
            // Move the gold prefab upwards by 5 units over 3 seconds
            StartCoroutine(MoveUpward(transform, 0.5f, goldDuration));
            if (targetPrefab != null)
            {
                // Move the target prefab upwards by 100 units over 5 seconds
                StartCoroutine(MoveTargetUpward(targetPrefab.transform, targetDuration));
            }
            hasMoved = true; // Prevent further movement
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

        objectTransform.position = end; // Ensure the final position is set accurately
    }

    IEnumerator MoveTargetUpward(Transform targetTransform, float duration)
    {
        Vector3 startPosition = targetTransform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + 115f, startPosition.z);
        float elapsed = 0f; // Time elapsed since the start of the movement

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            targetTransform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            yield return null;
        }

        targetTransform.position = endPosition; // Ensure the final position is set accurately
    }
}
