using UnityEngine;

public static class MinimapUtils
{
    public static Vector2 GetPositionOnMinimap(Camera camera, RectTransform minimapImage, Vector3 playerPos, Vector3 destinationPos, bool clamp = true)
    {
        Vector3 posDiff = GetPosDiff(playerPos, destinationPos);
        float cameraSize = camera.orthographicSize;

        float xPos = posDiff.x / cameraSize;
        float yPos = posDiff.z / cameraSize;

        if (clamp)
        {
            xPos = Mathf.Clamp(xPos, -1f, 1f);
            yPos = Mathf.Clamp(yPos, -1f, 1f);
        }

        return new Vector2(xPos * minimapImage.rect.width / 2, yPos * minimapImage.rect.height / 2);
    }

    public static Vector2 GetPosPercentage(RectTransform minimapRect, RectTransform otherRect)
    {
        Vector2 posDiff = GetUnbiasedPosition(otherRect) - GetUnbiasedPosition(minimapRect);

        float xPercentage = posDiff.x / minimapRect.sizeDelta.x;
        float yPercentage = posDiff.y / minimapRect.sizeDelta.y;

        Debug.Log($"[GetPosPercentage] - minimap rect pos (unbiased): {GetUnbiasedPosition(minimapRect)} player icon rect pos: {otherRect.anchoredPosition}");
        Debug.Log($"[GetPosPercentage] - pivot: {new Vector2(xPercentage, yPercentage)}");

        return new Vector2(xPercentage, yPercentage);
    }

    public static Vector3 GetPosDiff(Vector3 origin, Vector3 destination, bool flatten = true)
    {
        Vector3 flattenedOriginVector = new Vector3(origin.x, 0f, origin.z);
        Vector3 flattenedDestinationVector = new Vector3(destination.x, 0f, destination.z);

        if (flatten) return flattenedDestinationVector - flattenedOriginVector;

        return destination - origin;
    }

    public static Vector2 GetUnbiasedPosition(RectTransform rectTransform)
    {
        return rectTransform.anchoredPosition - Vector2.Scale(rectTransform.pivot, rectTransform.sizeDelta);
    }
}