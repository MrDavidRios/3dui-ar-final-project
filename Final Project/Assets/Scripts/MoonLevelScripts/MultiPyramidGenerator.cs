using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPyramidGenerator : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject goldWallEPrefab; // Gold WallE to be placed in one of the pyramids
    public GameObject terrainBounds; // Rectangular GameObject representing terrain bounds
    public Transform player;

    public int numberOfPyramids = 3;
    public float minimumDistanceBetweenPyramids = 10f;
    public float minimumDistanceFromPlayer = 20f;
    public int pyramidLayers = 5;
    public int baseLayerSize = 5;
    public float spacing = 1f; // Spacing between pyramid objects

    private List<Vector3> pyramidLocations = new List<Vector3>(); // List of pyramid positions

    public List<Transform> rocks;

    void Start()
    {
        if (terrainBounds == null)
        {
            Debug.LogError("Terrain bounds not set.");
            return;
        }

        CreatePyramids();
    }

    private void CreatePyramids()
    {
        Bounds bounds = terrainBounds.GetComponent<Renderer>().bounds; // Get the bounds of the terrain
        int randomPyramidIndex = Random.Range(0, numberOfPyramids); // Random index for the gold WallE pyramid

        for (int i = 0; i < numberOfPyramids; i++)
        {
            Vector3 randomPosition = GenerateRandomPosition(bounds);

            while (IsTooCloseToOthers(randomPosition) || IsTooCloseToPlayer(randomPosition) || IsInRestrictedZone(randomPosition))
            {
                randomPosition = GenerateRandomPosition(bounds);
            }

            pyramidLocations.Add(randomPosition); // Store the position of this pyramid

            Debug.Log("Gold WallE in pyramid " + randomPosition);

            // Create a new pyramid at the generated position
            CreatePyramid(randomPosition, i == randomPyramidIndex);
        }
    }

    private void CreatePyramid(Vector3 centerPoint, bool containsGoldWallE)
    {
        // Create an empty parent GameObject to hold the entire pyramid - to be used for wayfinding indicator
        GameObject pyramidParent = new GameObject("Pyramid");
        pyramidParent.transform.position = centerPoint;
        pyramidParent.transform.parent = transform;

        // Adjust start position for the base layer
        Vector3 startPosition = centerPoint - new Vector3((baseLayerSize - 1) * spacing / 2, 0, (baseLayerSize - 1) * spacing / 2);

        // Random location for the gold WallE within this pyramid
        int goldLayer = Random.Range(0, pyramidLayers);
        int goldX = Random.Range(0, baseLayerSize - goldLayer);
        int goldZ = Random.Range(0, baseLayerSize - goldLayer);

        for (int layer = 0; layer < pyramidLayers; layer++)
        {
            int layerSize = baseLayerSize - layer;
            Vector3 layerOffset = new Vector3(layer * spacing / 2, layer * spacing, layer * spacing / 2);

            for (int x = 0; x < layerSize; x++)
            {
                for (int z = 0; z < layerSize; z++)
                {
                    Vector3 position = startPosition + new Vector3(x * spacing, 0, z * spacing) + layerOffset;

                    if (containsGoldWallE && layer == goldLayer && x == goldX && z == goldZ)
                    {
                        // Place the gold WallE at the randomly chosen position
                        GameObject goldWallE = Instantiate(goldWallEPrefab, position, Quaternion.identity, pyramidParent.transform);
                        goldWallE.SetActive(true);
                    }
                    else
                    {
                        // Create a regular objectPrefab for the pyramid
                        Instantiate(objectPrefab, position, Quaternion.identity, pyramidParent.transform);
                    }
                }
            }
        }
    }

    private Vector3 GenerateRandomPosition(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        float y = bounds.min.y;

        return new Vector3(randomX, y, randomZ);
    }

    private bool IsTooCloseToOthers(Vector3 position)
    {
        foreach (var loc in pyramidLocations)
        {
            if (Vector3.Distance(loc, position) < minimumDistanceBetweenPyramids)
            {
                return true; // Too close to another pyramid
            }
        }
        return false; // Valid position
    }

    private bool IsTooCloseToPlayer(Vector3 position)
    {
        return Vector3.Distance(player.position, position) < minimumDistanceFromPlayer;
    }

    private bool IsInRestrictedZone(Vector3 position)
    {
        foreach (Transform zone in rocks)
        {
            if (zone.GetComponent<Collider>().bounds.Contains(position))
            {
                return true;
            }
        }
        return false;
    }
}