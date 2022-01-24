using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Camera Configuration")]
public class CameraConfiguration : ScriptableObject
{
    public float yaw;
    public float pitch;
    public float roll;
    public Vector3 pivot;
    public float distance;
    public float fov;

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(yaw, pitch, roll);
    }

    public Vector3 GetPosition()
    {
        //return pivot + ?offset
        return pivot;
    }
}
