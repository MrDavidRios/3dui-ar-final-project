using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestructionTracker : ObjectiveTracker
{
    public bool trackSpecificObjects;

    public GameObject[] objectsToTrack;

    public GameObject objectParent;

    //void Update()
    //{
    //    GameObject[] relevantGameObjects = GetRelevantGameObjects();
    //    bool relevantObjectsDestroyed = CheckIfObjectsAreDestroyed(relevantGameObjects);

    //    if (relevantObjectsDestroyed)
    //        OnObjectiveCompleted();
    //}

    protected GameObject[] GetRelevantGameObjects()
    {
        return trackSpecificObjects ? objectsToTrack : GetChildren(objectParent);
    }

    protected virtual bool CheckIfObjectsAreDestroyed(GameObject[] gameObjects)
    {
        // TODO: add logic to check if objects are destroyed
        // ===
        // this depends on the implementation of object destruction

        // Can extend in individual managers
        return false;
    }

    protected GameObject[] GetChildren(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];
        Debug.Log(parent.transform.childCount);

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }

        return children;
    }
}