using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSetup : MonoBehaviour
{

    public GameObject wallEPrefab;
    public GameObject goldWallEPrefab;
    public GameObject terrainBounds;
    public Transform player;

    public int numberOfWallEs = 10;
    public float minDistanceBetweenWallEs = 10f;
    public float minDistanceFromPlayer = 20f;

    private List<Vector3> wallELocations = new List<Vector3>();

    public float minY = 0.5f; // Minimum y position
    public float maxY = 2f; // Maximum y position

    private void Start()
    {
        if (terrainBounds == null)
        {
            Debug.LogError("Terrain bounds not set.");
            return;
        }

        CreateWallEs();
    }

    private void CreateWallEs()
    {
        Bounds bounds = terrainBounds.GetComponent<Renderer>().bounds;
        //int randomWallEIndex = Random.Range(0, numberOfWallEs); // generate rand index for goldenWallE

        // Not including golden wall-E right now

        int randomWallEIndex = -1;

        for (int i = 0; i < numberOfWallEs; i++)
        {
            Vector3 randomPosition = GenerateRandomPosition(bounds);

            while (IsTooCloseToOthers(randomPosition) || IsTooCloseToPlayer(randomPosition))
            {
                randomPosition = GenerateRandomPosition(bounds); // generate random position within bounds
            }

            wallELocations.Add(randomPosition); // Store the position of this Wall-E
            CreateWallE(randomPosition, i == randomWallEIndex);
        }
    }

    private void CreateWallE(Vector3 position, bool isGold)
    {
        //Quaternion randomRotation = Quaternion.Euler(Random.Range(-10f, 10f), Random.Range(0f, 360f), Random.Range(-10f, 10f));
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        GameObject wallEInstance = isGold ? Instantiate(goldWallEPrefab, position, randomRotation)
                                          : Instantiate(wallEPrefab, position, randomRotation);
        wallEInstance.transform.parent = transform;
    }


    private Vector3 GenerateRandomPosition(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        float y = Mathf.Clamp(bounds.min.y + Random.Range(1f, 5f), minY, maxY); // clamp within acceptable range

        return new Vector3(randomX, y, randomZ);
    }

    private bool IsTooCloseToOthers(Vector3 position)
    {
        foreach (var loc in wallELocations)
        {
            if (Vector3.Distance(loc, position) < minDistanceBetweenWallEs)
            {
                return true; // Too close to another Wall-E
            }
        }
        return false; // Valid position
    }

    private bool IsTooCloseToPlayer(Vector3 position)
    {
        return Vector3.Distance(player.position, position) < minDistanceFromPlayer;
    }
}
