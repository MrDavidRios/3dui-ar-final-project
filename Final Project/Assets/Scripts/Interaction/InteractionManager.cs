using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction;


public class InteractionManager : MonoBehaviour
{
    void Awake()
    {
        PointableUnityEventWrapper[] pointables = FindObjectsOfType<PointableUnityEventWrapper>();
        foreach (var pointable in pointables)
        {
            UnityAction<PointerEvent> onSelectAction =
                (eventData) => OnSelected(pointable.gameObject, eventData);
            UnityAction<PointerEvent> onUnselectAction =
                (eventData) => OnUnselected(pointable.gameObject, eventData);

            pointable.WhenSelect.AddListener(onSelectAction);
            pointable.WhenUnselect.AddListener(onUnselectAction);
        }
    }


    public void VibrateController(float amplitude, float duration, bool isRight)
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        if (isRight)
            UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.RightHanded, devices);
        else
            UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.LeftHanded, devices);

        uint channel = 0;
        foreach (var device in devices)
        {
            device.SendHapticImpulse(channel, amplitude, duration);
            Debug.Log("Haptic impulse sent to right hand controller" + device);
        }
    }

    private void OnSelected(GameObject go, PointerEvent eventData)
    {
        Debug.Log($"{go.name} was selected", go);
        
        GrabInteractor grabInteractor = eventData.Data as GrabInteractor;
        bool isRight = grabInteractor.transform.parent.parent.name == "RightController";
        
        VibrateController(0.5f, 0.05f, isRight);
    }

    private void OnUnselected(GameObject go, PointerEvent eventData)
    {
        Debug.Log($"{go.name} was released", go);
        
        GrabInteractor grabInteractor = eventData.Data as GrabInteractor;
        bool isRight = grabInteractor.transform.parent.parent.name == "RightController";
    }
}