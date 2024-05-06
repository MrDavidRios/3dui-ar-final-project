using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceOnProximity : MonoBehaviour
{
    public GameObject interactableWallEPrefab;
    public float forceThreshold = 75f;  // Threshold for the collision force to trigger replacement
    public enum SpawnType { Floating, Pyramid }  
    public SpawnType spawnType = SpawnType.Floating;  

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Breaker"))
        {
            
            float collisionForce = collision.impulse.magnitude / Time.fixedDeltaTime;

            
            if (collisionForce > forceThreshold)
            {
                
                Vector3 hitPoint = collision.contacts[0].point;
                ReplaceWithInteractableWallE(hitPoint);
            }
        }
    }

    void ReplaceWithInteractableWallE(Vector3 hitPosition)
    {
        
        Quaternion spawnRotation = transform.rotation;
        if (spawnType == SpawnType.Pyramid)
        {
            spawnRotation *= Quaternion.Euler(0, 180, 0);  // Apply 180-degree rotation on x-axis for Pyramid type
        }

        GameObject interactableWallE = Instantiate(interactableWallEPrefab, hitPosition, spawnRotation);

       
        Rigidbody newRb = interactableWallE.GetComponent<Rigidbody>();
        if (newRb != null)
        {
            newRb.isKinematic = true;  
        }

        
        Destroy(gameObject);
    }
}
