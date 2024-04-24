using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectiveTracker : MonoBehaviour
{
    public static Action ObjectiveCompleted;
    public static Action<int> ObjectiveCompletedWithScore; // Event with score parameter

    protected void OnObjectiveCompleted()
    {
        ObjectiveCompleted?.Invoke();
    }

    protected void OnObjectiveCompleted(int score)
    {
        ObjectiveCompletedWithScore?.Invoke(score);
    }
}
