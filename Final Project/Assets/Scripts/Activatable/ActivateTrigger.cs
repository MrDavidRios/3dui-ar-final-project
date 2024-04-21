using UnityEngine;

public class ActivateTrigger : MonoBehaviour
{
    [SerializeField] private GameObject activatableObject;
    private Activatable activatableComponent;

    public void Start()
    {
        activatableComponent = activatableObject.GetComponent<Activatable>();
        if (activatableComponent == null)
            throw new MissingComponentException($"Missing Activatable component on {activatableObject.name}");
    }

    public void Activate() => activatableComponent.Activate();

    public void Deactivate() => activatableComponent.Deactivate();
}
