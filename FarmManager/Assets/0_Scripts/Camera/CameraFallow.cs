using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 _offset;

    private void LateUpdate()
    {
        Vector3 desirePosition = target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
