using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : ObjectDestructionTracker
{

    private int totalCount = 0;

    void Start()
    {
        // find all breakable objects in scene
        GameObject[] relevantGameObjects = GetRelevantGameObjects();

        totalCount = relevantGameObjects.Length;

        //Debug.Log(totalCount);

        foreach(GameObject gameObject in relevantGameObjects)
        {
            Breakable breakable = gameObject.GetComponent<Breakable>();
            if (breakable != null)
            {
                breakable.OnBreak.AddListener(DecrementCount); // Add event listener to OnBreaks
            }
        }

    }

    public void DecrementCount()
    {
        totalCount -= 1;
        //Debug.Log(totalCount);

        if (totalCount <= 0)
        {
            OnObjectiveCompleted(); // trigger completion event - bring up menu? 
        }
    }
}
