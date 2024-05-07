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
    [SerializeField] private RectTransform minimapRect;

    private void Update()
    {
        SortedList<float, Transform> piles = new SortedList<float, Transform>();
        for (int i = 0; i < pyramidsContainer.childCount; i++)
        {
            Transform pyramidParent = pyramidsContainer.GetChild(i);
            float distToPyramid = MinimapUtils.GetPosDiff(player.transform.position, pyramidParent.transform.position).magnitude;
            piles.Add(distToPyramid, pyramidParent);
        }

        if (piles.Values.Count > 0)
            nextPile = piles.Values[0];
        else
            nextPile = null;

        playerIcon.localRotation = Quaternion.Euler(0f, 0f, -playerIcon.parent.rotation.eulerAngles.y);
    }

    private void LateUpdate()
    {
        playerIcon.anchoredPosition = MinimapUtils.GetPositionOnMinimap(minimapCamera, minimapRect, player.position, player.position);
        ghostWallEIcon.anchoredPosition = MinimapUtils.GetPositionOnMinimap(minimapCamera, minimapRect, player.position, ghostWallE.position);

        // Only show the next pile icon if there is a next pile in sight.
        pileIcon.gameObject.SetActive(nextPile != null);
        if (nextPile)
            pileIcon.anchoredPosition = MinimapUtils.GetPositionOnMinimap(minimapCamera, minimapRect, player.position, nextPile.position);
    }
}
