using System;
using UnityEngine;

public class PauseFunctionality : MonoBehaviour
{
    public static Action GamePaused;
    public static Action GameResumed;

    public void PauseGame()
    {
        GamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        GameResumed?.Invoke();
    }
}
