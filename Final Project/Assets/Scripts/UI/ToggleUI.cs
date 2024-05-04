using System;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    private Canvas[] canvases;
    private void Start()
    {
        canvases = FindObjectsOfType<Canvas>(); 
    }

    public void Enable(GameObject UIObject)
    {
        foreach (Canvas canvas in canvases)
        {
            var canvasTransform = canvas.GetComponent<RectTransform>();
            canvasTransform.position = Camera.main.transform.position;
            canvasTransform.rotation = Camera.main.transform.rotation;
        }
        UIObject.SetActive(true);
    }

    public void Disable(GameObject UIObject)
    {
        UIObject.SetActive(false);
    }

    public void Toggle(GameObject UIObject)
    {
        UIObject.SetActive(!UIObject.activeInHierarchy);
    }
}
