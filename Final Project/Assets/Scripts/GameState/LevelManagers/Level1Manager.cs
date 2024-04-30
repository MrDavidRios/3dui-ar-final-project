using UnityEngine;

public class Level1Manager : ObjectDestructionTracker
{
    private int totalObjectsDestroyed = 0;
    private int breakableObjectAmount = 0;

    void Start()
    {
        // Find all breakable objects in scene
        GameObject[] relevantGameObjects = GetRelevantGameObjects();
        breakableObjectAmount = relevantGameObjects.Length;

        foreach (GameObject gameObject in relevantGameObjects)
        {
            Breakable breakable = gameObject.GetComponentInChildren<Breakable>();
            if (breakable != null)
            {
                breakable.OnBreak.AddListener(OnObjectBroken); // Add event listener to OnBreaks
            }
        }

        Debug.Log("Total number of vases: " + breakableObjectAmount);

    }

    public void OnObjectBroken()
    {
        totalObjectsDestroyed++;

        Debug.Log("totalObjectsDestroyed:" + totalObjectsDestroyed);

        if (breakableObjectAmount - totalObjectsDestroyed <= 0)
        {
            Debug.Log("REACHED");
            OnObjectiveCompleted(breakableObjectAmount);
        }
    }
}
