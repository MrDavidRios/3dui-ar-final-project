using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void OnEnable()
    {
        PauseFunctionality.GamePaused += OnPause;
        PauseFunctionality.GameResumed += OnResume;
    }

    private void OnDisable()
    {
        PauseFunctionality.GamePaused -= OnPause;
        PauseFunctionality.GameResumed -= OnResume;
    }

    private void OnPause()
    {
        Time.timeScale = 0;
    }

    private void OnResume()
    {
        Time.timeScale = 1;
    }
}
