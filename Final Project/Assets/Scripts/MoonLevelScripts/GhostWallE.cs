using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWallE : MonoBehaviour
{
    // Scripts controls the movement of ghost Wall-E (wayfinding support)

    // The distances between the assigned transforms are used to determine
    // events.

    // If the player is more than followThreshold distance away from the ghost,
    // the ghost gameObject will stop moving and face the player. A prompt will also be rendered.

    // Once the ghost reaches the end point (i.e., mountain), it will stop translating and
    // constantly rotate towards the user and prompt the user to find its body. 

    public Transform player;
    public Transform mountain;

    public GameObject followMePrompt;
    public GameObject findMePrompt;

    public float followThreshold = 5.0f;
    public float distanceToMountainThreshold = 5.0f;

    public float moveSpeed = 0.5f;
    public float turnSpeed = 0.5f;

    private bool mountainReached = false;

    void Start()
    {
        followMePrompt.SetActive(true);
        findMePrompt.SetActive(false);

        FacePlayer();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //Debug.Log("Distance to player:");Debug.Log(distanceToPlayer);

        if (mountainReached)
        {
            FacePlayer();
            return;
        }

        if (distanceToPlayer > followThreshold)
        {
            FacePlayer();
            followMePrompt.SetActive(true);
        }
        else
        {
            if (!mountainReached)
            {
                MoveTowardsMountain();
                followMePrompt.SetActive(false);
            }
        }
    }

    public void MoveTowardsMountain()
    {
        Vector3 direction = (mountain.position - transform.position).normalized;
        float distanceToMountain = Vector3.Distance(transform.position, mountain.position);

        if (distanceToMountain > distanceToMountainThreshold)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

            transform.position += moveSpeed * Time.deltaTime * direction; // Move towards mountain
        } else
        {
            mountainReached = true;
            findMePrompt.SetActive(true);
        }
    }

    public void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }

    // Could introduce another function for when Wall-E has been found.

}
