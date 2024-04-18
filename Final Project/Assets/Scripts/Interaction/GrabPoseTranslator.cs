using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class GrabPoseTranslator : MonoBehaviour
{
    public GameObject grabObj;
    private Transform grabPose;
    void Start()
    {
        // grabPose = grabObj.transform.Find("GrabPose");
    }
    
    public void SetGrabPose(PointerEvent eventData)
    {
        // GrabInteractor grabInteractor = eventData.Data as GrabInteractor;
        // var handPose = grabInteractor.transform.position;
        // var pos_diff = handPose - grabPose.position;
        // // grabObj.transform.position += pos_diff;
        //
        // var rot_diff = Quaternion.Inverse(grabInteractor.transform.rotation) * grabPose.rotation;
        // grabObj.transform.rotation *= rot_diff;
    }
    

}
