using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnCamera : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.rotation = Camera.main.transform.rotation;
        rectTransform.position = Camera.main.transform.position;
    }
}
