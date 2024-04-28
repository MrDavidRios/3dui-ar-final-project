using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Transform destination;

    public GameObject followMePrompt;
    public GameObject findMePrompt;

    public float followThreshold = 5.0f;
    public float distanceToMountainThreshold = 5.0f;

    public float moveSpeed = 0.5f;
    public float turnSpeed = 0.5f;

    public float fadeSpeed = 0.1f;

    private bool destinationReached = false;

    void Start()
    {
        followMePrompt.SetActive(true);
        findMePrompt.SetActive(false);
        //FadeIn();
        FacePlayer();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //Debug.Log("Distance to player:");Debug.Log(distanceToPlayer);

        if (destinationReached)
        {
            FadeOut();
            //FacePlayer();
            Deactivate();
        } else
        {
            FadeIn();
        }

        if (distanceToPlayer > followThreshold)
        {
            FacePlayer();
            followMePrompt.SetActive(true);
        }
        else
        {
            if (!destinationReached)
            {
                MoveTowardsDestination();
                followMePrompt.SetActive(false);
            }
        }
    }

    public void Deactivate()
    {
        if (this.GetComponent<Renderer>().material.color.a <= 0)
            gameObject.SetActive(false);
        if (followMePrompt.GetComponentInChildren<TextMeshProUGUI>().color.a <= 0 && followMePrompt.GetComponent<Image>().color.a <= 0)
            followMePrompt.SetActive(false);
    }

    public void MoveTowardsDestination()
    {
        Vector3 direction = (destination.position - transform.position).normalized;
        float distanceToMountain = Vector3.Distance(transform.position, destination.position);

        if (distanceToMountain > distanceToMountainThreshold)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

            transform.position += moveSpeed * Time.deltaTime * direction; // Move towards mountain
        } else
        {
            destinationReached = true;
        }
    }

    public void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }

    // -------------------FadeOut() code-------------------

    public void FadeOut()
    {
        StartCoroutine(FadeOutObject());
        StartCoroutine(FadeOutUIImage());
        StartCoroutine(FadeOutUIText());
    }

    public IEnumerator FadeOutObject()
    {
        while (this.GetComponent<Renderer>().material.color.a > 0)
        {
            Color objectColor = this.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed*Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;
            yield return null;
        }
    }

    public IEnumerator FadeOutUIImage()
    {

        while (followMePrompt.GetComponent<Image>().color.a > 0)
        {
            Color objectColor = followMePrompt.GetComponent<Image>().color;
            float fadeAmount = objectColor.a - (2*fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            followMePrompt.GetComponent<Image>().color = objectColor;
            yield return null;
        }
    }

    public IEnumerator FadeOutUIText()
    {

        while (followMePrompt.GetComponentInChildren<TextMeshProUGUI>().color.a > 0)
        {
            Color objectColor = followMePrompt.GetComponentInChildren<TextMeshProUGUI>().color;
            float fadeAmount = objectColor.a - (2*fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            followMePrompt.GetComponentInChildren<TextMeshProUGUI>().color = objectColor;
            yield return null;
        }
    }

    // -------------------FadeIn() code-------------------

    public void FadeIn()
    {
        StartCoroutine(FadeInObject());
        StartCoroutine(FadeInUIImage());
        StartCoroutine(FadeInUIText());
    }

    public IEnumerator FadeInObject()
    {
        while (this.GetComponent<Renderer>().material.color.a < 0.5f)
        {
            Color objectColor = this.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime *0.5f);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;
            yield return null;
        }
    }

    public IEnumerator FadeInUIImage()
    {

        while (followMePrompt.GetComponent<Image>().color.a < 0.6f)
        {
            Color objectColor = followMePrompt.GetComponent<Image>().color;
            float fadeAmount = objectColor.a + (2 * fadeSpeed * Time.deltaTime * 0.5f);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            followMePrompt.GetComponent<Image>().color = objectColor;
            yield return null;
        }
    }

    public IEnumerator FadeInUIText()
    {

        while (followMePrompt.GetComponentInChildren<TextMeshProUGUI>().color.a < 0.6f)
        {
            Color objectColor = followMePrompt.GetComponentInChildren<TextMeshProUGUI>().color;
            float fadeAmount = objectColor.a + (2 * fadeSpeed * Time.deltaTime * 0.5f);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            followMePrompt.GetComponentInChildren<TextMeshProUGUI>().color = objectColor;
            yield return null;
        }
    }
}
