using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");   
        canvas.GetComponent<RectTransform>().position = Camera.main.transform.position + Camera.main.transform.forward * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
