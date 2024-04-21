

using UnityEngine;

/// <summary>
/// This script is written with the intention of being used on an empty parent of UI components. This makes activation/
/// deactivation functionality as generalizable as possible.
/// </summary>
public class UIActivatable : Activatable
{
    override public void Activate()
    {
        gameObject.SetActive(true);
    }

    override public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}