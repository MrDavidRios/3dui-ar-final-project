using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTracker : ObjectiveTracker
{

    public TMP_Text timeText;

    private float timeRemaining = -100f;
    private int score = 0; // to pass from Level2Manager to OnObjectiveCompleted()

    private void OnEnable() => Countdown.CountdownStarted += SetTimeRemaining;

    private void OnDisable() => Countdown.CountdownStarted -= SetTimeRemaining;

    public void SetTimeRemaining(float timeAmount)
    {
        timeRemaining = timeAmount;
        timeText.text = timeRemaining.ToString();
    }

    public void SetScore(int i)
    {
        score = i;
    }

    private void Update()
    {
        if(timeRemaining <= 0f)
            OnObjectiveCompleted(score);
        else
            timeRemaining -= Time.deltaTime;
        timeText.text = ((int)timeRemaining).ToString();

        Debug.Log("Time: " + timeRemaining);
    }
}
