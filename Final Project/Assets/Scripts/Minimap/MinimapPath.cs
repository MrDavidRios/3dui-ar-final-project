using UnityEngine;

public class MinimapPath : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private RectTransform pathObj;

    [Header("Minimap Objects")]
    [SerializeField] private Camera minimapCamera;
    [SerializeField] private RectTransform minimapRect;
    [SerializeField] private RectTransform pivotPoint;

    [SerializeField] private GameObject pathLinePrefab;

    public float distanceUntilNextLine;

    private Vector3 lastPos;
    private Vector3 origin;

    private void Start()
    {
        origin = lastPos = player.transform.position;
    }

    void Update()
    {
        float dist = MinimapUtils.GetPosDiff(player.transform.position, lastPos).magnitude;

        pathObj.anchoredPosition = MinimapUtils.GetPositionOnMinimap(minimapCamera, minimapRect, player.transform.position, origin, false);
        Debug.Log($"Minimap anchored position: {pathObj.anchoredPosition}");

        if (dist >= distanceUntilNextLine)
        {
            GameObject newLine = Instantiate(pathLinePrefab, pathObj.transform);
            RectTransform newLineRect = newLine.GetComponent<RectTransform>();

            Vector2 currentPosOnMinimap = MinimapUtils.GetPositionOnMinimap(minimapCamera, minimapRect, player.transform.position, player.transform.position, false);
            Vector2 lastPosOnMinimap = MinimapUtils.GetPositionOnMinimap(minimapCamera, minimapRect, player.transform.position, lastPos, false);

            Vector2 lineCenterPos = (currentPosOnMinimap + lastPosOnMinimap) / 2;

            newLineRect.anchoredPosition = lineCenterPos - pathObj.anchoredPosition;

            Vector2 dir = currentPosOnMinimap - lastPosOnMinimap;
            newLineRect.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + pivotPoint.rotation.eulerAngles.z);

            // Configure line length (rect transform width)
            float lineWidth = Vector2.Distance(currentPosOnMinimap, lastPosOnMinimap);
            newLineRect.sizeDelta = new Vector2(lineWidth, newLineRect.sizeDelta.y);

            lastPos = player.transform.position;
        }
    }
}
