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
    
    private bool IsRightHand(PointerEvent eventData)
    {
        bool isRight = false;
        if (eventData.Data.GetType() == typeof(GrabInteractor))
        {
            GrabInteractor grabInteractor = eventData.Data as GrabInteractor;
            isRight = grabInteractor.transform.parent.parent.name == "RightController";    
        }
        else if(eventData.Data.GetType() == typeof(DistanceGrabInteractor))
        {
            DistanceGrabInteractor distanceGrabInteractor = eventData.Data as DistanceGrabInteractor;
            isRight = distanceGrabInteractor.transform.parent.parent.name == "RightController";
        }
        else
        {
            Debug.LogError("Unknown type pointerevent " + eventData.Data.GetType());
            isRight = false;
        }

        return isRight;
    }

    private void OnSelected(GameObject go, PointerEvent eventData)
    {
        Debug.Log($"{go.name} was selected", go);
        bool isRight = IsRightHand(eventData);
        VibrateController(0.5f, 0.05f, isRight);

        try
        {
            Breaker collider = go.transform.Find("Visuals/Root/Collider").gameObject.GetComponent<Breaker>();
            collider.GrabbedHand = isRight ? OVRHand.Hand.HandRight : OVRHand.Hand.HandLeft;
        }
        catch
        {
            Debug.Log("No collider object found");
        }
    }

    private void OnUnselected(GameObject go, PointerEvent eventData)
    {
        Debug.Log($"{go.name} was released", go);
        bool isRight =  IsRightHand(eventData);
        
        try
        {
            Breaker collider = go.transform.Find("Visuals/Root/Collider").gameObject.GetComponent<Breaker>();
            collider.GrabbedHand = OVRHand.Hand.None;
        }
        catch
        {
            Debug.Log("No collider object found");
        }
    }
}