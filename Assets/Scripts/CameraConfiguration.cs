using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraConfiguration
{
    public CameraConfiguration(Transform cameraTransform)
    {
        this.cameraTransform = cameraTransform;
    }

    public float yaw;
    public float pitch;
    public float roll;
    public Vector3 pivot;
    public float distance;
    public float fov;
    public Transform cameraTransform;

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(yaw, pitch, roll);
    }

    public Vector3 GetPosition()
    {
        return GetRotation() * (Vector3.back * distance) + cameraTransform.position;
    }
}
