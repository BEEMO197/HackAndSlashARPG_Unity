using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 updatedPosition = followTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, updatedPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(followTarget);
    }
}
