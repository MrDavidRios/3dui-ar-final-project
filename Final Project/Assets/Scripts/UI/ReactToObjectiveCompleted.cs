using UnityEngine;
using UnityEngine.Events;

public class ReactToObjectiveCompleted : MonoBehaviour
{
    [Header("On Objective Completed")]
    public UnityEvent reaction;

    private void OnEnable()
    {
        ObjectiveTracker.ObjectiveCompleted += React;
    }

    private void OnDisable()
    {
        ObjectiveTracker.ObjectiveCompleted -= React;
    }

    private void React()
    {
        reaction?.Invoke();
    }
}