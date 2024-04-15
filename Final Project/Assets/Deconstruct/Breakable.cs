using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject originalObject;
    [SerializeField] GameObject brokenObject;

    BoxCollider bc;
    private void Awake()
    {
        originalObject.SetActive(true);
        brokenObject.SetActive(false);

        bc = GetComponent<BoxCollider>();

    }

    private void OnMouseDown()
    {
        Break();
    }


    private void Break()
    {
        originalObject.SetActive(false);
        brokenObject.SetActive(true);

        bc.enabled = false;
    }
}
