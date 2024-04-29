using UnityEngine;
using UnityEngine.Events;

public class ReactToObjective : MonoBehaviour
{
    [Header("On Objective Completed")]
    public UnityEvent completionReaction;

    [Header("On Objective Failed")]
    public UnityEvent failureReaction;

    private void OnEnable()
    {
        ObjectiveTracker.ObjectiveCompleted += OnCompletedReaction;
        ObjectiveTracker.ObjectiveFailed += OnFailureReaction;
    }

    private void OnDisable()
    {
        ObjectiveTracker.ObjectiveCompleted -= OnCompletedReaction;
        ObjectiveTracker.ObjectiveFailed -= OnFailureReaction;
    }

    private void OnCompletedReaction(int score)
    {
        completionReaction?.Invoke();
    }

    private void OnFailureReaction(int score)
    {
        failureReaction?.Invoke();
    }
}