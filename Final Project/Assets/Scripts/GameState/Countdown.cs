using System;
using UnityEngine;

/** 
 * Responsible for letting other scripts know that a countdown has started.
*/
public class Countdown : MonoBehaviour
{
    [Tooltip("The amount of time (in seconds) given to the user to complete the level.")]
    [SerializeField] private float time = 60f;

    public static Action<float> CountdownStarted;

    public void StartCountdown() => CountdownStarted?.Invoke(time);
}
