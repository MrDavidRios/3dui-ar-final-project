using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithPlayerUI : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private RectTransform rectTransform;

    public float rotationOffset = 180f;

    private void LateUpdate()
    {
        rectTransform.rotation = Quaternion.Euler(rectTransform.eulerAngles.x, rectTransform.eulerAngles.y, player.eulerAngles.y + rotationOffset);
    }
}
