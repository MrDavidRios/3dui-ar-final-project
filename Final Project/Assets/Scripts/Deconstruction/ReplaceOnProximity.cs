using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceOnProximity : MonoBehaviour
{
    public GameObject interactableWallEPrefab;

    void OnCollisionEnter(Collision collision)
    {
        // Check for collision with the "Breaker"
        if (collision.gameObject.CompareTag("Breaker"))
        {
            ReplaceWithInteractableWallE();
        }
    }

    void ReplaceWithInteractableWallE()
    {
        // Instantiate the InteractableWallE prefab at the same position and rotation as this object
        GameObject interactableWallE = Instantiate(interactableWallEPrefab, transform.position, transform.rotation);
        
        // If the original object has a Rigidbody, transfer velocity to the new object
        Rigidbody originalRb = GetComponent<Rigidbody>();
        if (originalRb != null)
        {
            Rigidbody newRb = interactableWallE.GetComponent<Rigidbody>();
            if (newRb != null)
            {
                newRb.velocity = originalRb.velocity;
                newRb.angularVelocity = originalRb.angularVelocity;
            }
        }

        // Destroy this object
        Destroy(gameObject);
    }

}
