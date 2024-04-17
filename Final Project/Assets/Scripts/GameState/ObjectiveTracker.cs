using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectiveTracker : MonoBehaviour
{
    public static Action ObjectiveCompleted;

    protected void OnObjectiveCompleted()
    {
        ObjectiveCompleted?.Invoke();
    }
}
