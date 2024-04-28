using System.Collections.Generic;
using UnityEngine;

public class MinimapIcons : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform player;
    [SerializeField] private RectTransform playerIcon;

    [Header("Ghost Wall-E")]
    [SerializeField] private Transform ghostWallE;
    [SerializeField] private RectTransform ghostWallEIcon;

    [Header("Next Pile")]
    [SerializeField] private Transform pyramidsContainer;
    [SerializeField] private RectTransform pileIcon;
    private Transform nextPile;

    [Header("Minimap Objects")]
    [SerializeField] private Camera minimapCamera;
    [SerializeField] private RectTransform minimapImage;

    private void Update()
    {
        SortedList<float, Transform> piles = new SortedList<float, Transform>();
        for (int i = 0; i < pyramidsContainer.childCount; i++)
        {
            Transform pyramidParent = pyramidsContainer.GetChild(i);
            float distToPyramid = GetPosDiff(player.transform.position, pyramidParent.transform.position).magnitude;
            piles.Add(distToPyramid, pyramidParent);
        }

        if (piles.Values.Count > 0)
            nextPile = piles.Values[0];
        else
            nextPile = null;
    }

    private void LateUpdate()
    {
        playerIcon.anchoredPosition = GetRelativeIconPosition(player.position);
        ghostWallEIcon.anchoredPosition = GetRelativeIconPosition(ghostWallE.position);

        // Only show the next pile icon if there is a next pile in sight.
        pileIcon.gameObject.SetActive(nextPile != null);
        if (nextPile)
            pileIcon.anchoredPosition = GetRelativeIconPosition(nextPile.position);
    }

    private Vector2 GetRelativeIconPosition(Vector3 destinationPos)
    {
        Vector3 posDiff = GetPosDiff(player.transform.position, destinationPos);
        float cameraSize = minimapCamera.orthographicSize;

        float xPos = posDiff.x / cameraSize;
        float yPos = posDiff.z / cameraSize;

        xPos = Mathf.Clamp(xPos, -1f, 1f);
        yPos = Mathf.Clamp(yPos, -1f, 1f);

        return new Vector2(xPos * minimapImage.rect.width / 2, yPos * minimapImage.rect.height / 2);
    }

    private Vector3 GetPosDiff(Vector3 origin, Vector3 destination, bool flatten = true)
    {
        Vector3 flattenedOriginVector = new Vector3(origin.x, 0f, origin.z);
        Vector3 flattenedDestinationVector = new Vector3(destination.x, 0f, destination.z);

        if (flatten) return flattenedDestinationVector - flattenedOriginVector;

        return destination - origin;
    }
}
