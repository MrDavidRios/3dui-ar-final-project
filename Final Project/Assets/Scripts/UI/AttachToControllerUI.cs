using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToControllerUI : MonoBehaviour
{
    [SerializeField] private Transform controller;

    private RectTransform rectTransform;

    public Vector3 rotateBy;

    public Vector3 offset = new Vector3(0f, 0.18f, 0f);

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rectTransform.position = controller.position + controller.TransformDirection(offset);
        rectTransform.rotation = controller.rotation * Quaternion.Euler(rotateBy);
    }
}
