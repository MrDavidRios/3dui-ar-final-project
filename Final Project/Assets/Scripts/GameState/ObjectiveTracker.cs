using System;
using UnityEngine;

public abstract class ObjectiveTracker : MonoBehaviour
{
    // Objective Completed
    public static Action<int> ObjectiveCompleted;

    // Objective Failed
    public static Action<int> ObjectiveFailed;

    protected void OnObjectiveCompleted(int score)
    {
        ObjectiveCompleted?.Invoke(score);
    }

    protected void OnObjectiveFailed(int score)
    {
        ObjectiveFailed?.Invoke(score);
    }
}
