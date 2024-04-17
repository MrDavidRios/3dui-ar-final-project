using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTracker : ObjectiveTracker
{
    private float timeRemaining = -100f;

    private void OnEnable() => Countdown.CountdownStarted += SetTimeRemaining;

    private void OnDisable() => Countdown.CountdownStarted -= SetTimeRemaining;

    private void SetTimeRemaining(float timeAmount)
    {
        timeRemaining = timeAmount;
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
            OnObjectiveCompleted();
    }
}
