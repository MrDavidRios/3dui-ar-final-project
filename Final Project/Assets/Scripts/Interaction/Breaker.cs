using System.Collections;
using System.Collections.Generic;
using Oculus.Haptics;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    Vector3 lastPosition;
    Vector3 _velocity;
    public HapticClip hapticClip;
    private HapticClipPlayer _hapticClipPlayer;
    private OVRHand.Hand _grabbedHand = OVRHand.Hand.None;

    public Vector3 Velocity
    {
        get { return _velocity; }
    }
    
    public OVRHand.Hand GrabbedHand
    {
        get { return _grabbedHand; }
        set { _grabbedHand = value; }
    }
    
    public void HitObject()
    {
        if (hapticClip != null)
        {
            if(_grabbedHand == OVRHand.Hand.HandLeft)
                _hapticClipPlayer.Play(Controller.Left);
            else if(_grabbedHand == OVRHand.Hand.HandRight)
                _hapticClipPlayer.Play(Controller.Right);
            else
                Debug.Log("No hand set, " + gameObject.transform.parent.parent.parent.name);
        }
        else
        {
            Debug.LogError("Haptic clip not found");
        }
    }
    
    void Start()
    {
        _hapticClipPlayer = new HapticClipPlayer(hapticClip);
    }
    
    void Update()
    {
        _velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}
