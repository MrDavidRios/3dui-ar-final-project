using UnityEngine;

public class Level3Manager : ObjectiveTracker
{

    // Start

    // find the gold-Walle in the scene first.

    public void SetTutorialComplete()
    {
        Debug.Log("Golden Wall-E found!");
        OnObjectiveCompleted(0);
    }
}
