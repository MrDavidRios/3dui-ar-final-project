using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceOnProximity : MonoBehaviour
{
    public GameObject interactableWallEPrefab; 
    public float proximityDistance = 5f; 
    private GameObject breaker; 

    private int replacementCount = 0; 
    private float replacementTimer = 0f; 
    private bool canReplace = true;

    void Start()
    {
        breaker = GameObject.FindGameObjectWithTag("Breaker");
    }

    void Update()
    {
        if (breaker == null) return; 

        // Update the timer
        if (replacementTimer > 0)
        {
            replacementTimer -= Time.deltaTime;
        }
        else
        {
            // Reset count and allow replacements again after 5 seconds
            canReplace = true;
            replacementCount = 0;
        }

        if (canReplace && replacementCount < 3)
        {
            float distance = Vector3.Distance(breaker.transform.position, transform.position);
            if (distance <= proximityDistance)
            {
                ReplaceWithInteractableWallE();
                replacementCount++;
                if (replacementCount >= 3)
                {
                    // Start the 5-second timer after reaching the limit
                    replacementTimer = 5f;
                    canReplace = false;
                }
            }
        }
    }

    void ReplaceWithInteractableWallE()
    {
        GameObject interactableWallE = Instantiate(interactableWallEPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
