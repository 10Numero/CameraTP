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

    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(pivot, 0.25f);
        Vector3 position = GetPosition();
        Gizmos.DrawLine(pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, fov, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
