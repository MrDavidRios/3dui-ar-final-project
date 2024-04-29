using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public void Enable(GameObject UIObject)
    {
        UIObject.SetActive(true);
    }

    public void Disable(GameObject UIObject)
    {
        UIObject.SetActive(false);
    }

    public void Toggle(GameObject UIObject)
    {
        UIObject.SetActive(!UIObject.activeInHierarchy);
    }
}
