using UnityEngine;

public class Level2Manager : ObjectDestructionTracker
{
    private int totalCount = 0;
    private int hitCount = 0;

    [SerializeField] private TimeTracker timeTracker;
    [SerializeField] private float countdownTime = 60f;

    void Start()
    {
        // set up timer
        if (timeTracker != null)
        {
            timeTracker.gameObject.SetActive(true); // Enable TimeTracker
            timeTracker.SetTimeRemaining(countdownTime); // Set the countdown time
        }

        GameObject[] relevantGameObjects = GetRelevantGameObjects();

        totalCount = relevantGameObjects.Length;

        //Debug.Log(totalCount);

        foreach (GameObject gameObject in relevantGameObjects)
        {
            Breakable breakable = gameObject.GetComponent<Breakable>();
            if (breakable != null)
            {
                breakable.OnBreak.AddListener(DecrementCount); // Add event listener to OnBreak
                breakable.OnBreak.AddListener(IncrementScore); // Add event listener to OnBreak
            }
        }

        if (timeTracker != null)
        {
            timeTracker.SetScore(hitCount); // Set the initial score in TimeTracker
        }
    }

    public void DecrementCount()
    {
        totalCount -= 1;
        Debug.Log(totalCount);

        if (totalCount <= 0)
        {
            OnObjectiveCompleted(hitCount);
        }
    }

    public void IncrementScore()
    {
        hitCount += 1;
        if (timeTracker != null)
        {
            timeTracker.SetScore(hitCount); // Update the score in TimeTracker
        }
    }
}
