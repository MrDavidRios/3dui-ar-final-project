using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    Vector3 lastPosition;
    Vector3 _velocity;

    public Vector3 Velocity
    {
        get { return _velocity; }
    }
    void Update()
    {
        _velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}
