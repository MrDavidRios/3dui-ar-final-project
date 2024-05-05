using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceOnProximity : MonoBehaviour
{
    public GameObject interactableWallEPrefab; 
    public float proximityDistance = 5f; 
    private GameObject breaker; 

    void Start()
    {
        
        breaker = GameObject.FindGameObjectWithTag("Breaker");
    }

    void Update()
    {
        if (breaker == null) return; 

        
        float distance = Vector3.Distance(breaker.transform.position, transform.position);
        if (distance <= proximityDistance)
        {
            ReplaceWithInteractableWallE();
        }
    }

    void ReplaceWithInteractableWallE()
    {
       
        GameObject interactableWallE = Instantiate(interactableWallEPrefab, transform.position, transform.rotation);

        
        Destroy(gameObject);
    }
}
