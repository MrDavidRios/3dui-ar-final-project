using UnityEngine;
using TMPro;

public class TimeTracker : ObjectiveTracker
{
    public TMP_Text timeText;
    public TMP_Text scoreUIText;

    public Level2Manager level2Manger;

    private float timeRemaining = -100f;
    private int score = 0; // to pass from Level2Manager to OnObjectiveCompleted()

    private bool objectiveCompleted = false;

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

    public void StopCountdown()
    {
        objectiveCompleted = true;
    }

    private void Update()
    {
        if (objectiveCompleted)
        {
            return;
        }

        if (timeRemaining <= 0f)
        {
            level2Manger.MakeAllUnbreakable();
            scoreUIText.text = "You destroyed " + score + " objects!";
            OnObjectiveFailed(score);
            return;
        }

        timeRemaining -= Time.deltaTime;

        timeText.text = ((int)timeRemaining).ToString();
        Debug.Log("Time left: " + timeRemaining);
    }
}
