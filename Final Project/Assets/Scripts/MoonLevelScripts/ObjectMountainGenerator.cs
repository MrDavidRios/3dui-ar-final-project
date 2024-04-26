using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMountainGenerator : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform centerPoint;
    public int numObjects = 10;


    public GameObject goldWallE; // the gold wallE to hit and find

    public float placementRadius = 5f; // for RandomSpawn()

    public int layers = 5;
    public int baseLayerSize = 10; // Number of objects in base layer of pyramid
    public float spacing = 1.5f;

    private void Start()
    {
        //RandomSpawn();
        CreatePyramid();
    }

    private void RandomSpawn()
    /* Spawns numObjects objectPrefab at centerPoint randomly within placementRadius sphere */

    {
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 randomPos = Random.insideUnitSphere * placementRadius + centerPoint.position;

            Instantiate(objectPrefab, randomPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }

    private void CreatePyramid()
    {
        //float currentHeight = 0;
        //int currentLayerSize = baseSize;

        //for (int layer = 0; layer < layers; layer++)
        //{
        //    float radius = layer * spacing;
        //    for (int i = 0; i < currentLayerSize; i++)
        //    {
        //        float angle = i * Mathf.PI * 2 / currentLayerSize;
        //        Vector3 position = new Vector3(
        //            Mathf.Cos(angle) * radius,
        //            currentHeight,
        //            Mathf.Sin(angle) * radius
        //        ) + centerPoint.position;

        //        Instantiate(objectPrefab, position, Quaternion.identity, centerPoint);
        //    }
        //    currentHeight += spacing;
        //    currentLayerSize = Mathf.Max(1, currentLayerSize - 1);
        //}


        // randomize a place in the pyramid to instantiate the gold WallE


        Vector3 startPosition = centerPoint.position - new Vector3((baseLayerSize - 1) * spacing / 2, 0, (baseLayerSize - 1) * spacing / 2);

        int goldLayer = Random.Range(0, layers); // Random layer for gold WallE
        int goldX = Random.Range(0, baseLayerSize - goldLayer); // Random X within the chosen layer
        int goldZ = Random.Range(0, baseLayerSize - goldLayer); // Random Z within the chosen layer

        for (int layer = 0; layer < layers; layer++)
        {
            int layerSize = baseLayerSize - layer; // Decrease grid size by 1 for each upper layer
            Vector3 layerOffset = new Vector3(layer * spacing / 2, layer * spacing, layer * spacing / 2);

            for (int x = 0; x < layerSize; x++)
            {
                for (int z = 0; z < layerSize; z++)
                {
                    Vector3 position = startPosition + new Vector3(x * spacing, 0, z * spacing) + layerOffset;
                    if (layer == goldLayer && x == goldX && z == goldZ)
                    {
                        // Instantiate the gold WallE at this position
                        Instantiate(goldWallE, position, Quaternion.identity, centerPoint);
                    }
                    else
                    {
                        // Instantiate a regular objectPrefab
                        Instantiate(objectPrefab, position, Quaternion.identity, centerPoint);
                    }
                }
            }
        }
    }
}
