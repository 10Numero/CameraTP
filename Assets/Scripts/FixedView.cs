using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedView : AView
{
    public float yaw;
    public float pitch;
    public float roll;
    public float fov;

    public override CameraConfiguration GetConfiguration()
    {
        currentConfiguration.yaw = yaw;
        currentConfiguration.pitch = pitch;
        currentConfiguration.roll = roll;
        currentConfiguration.fov = fov;
        currentConfiguration.pivot = transform.position;
        currentConfiguration.distance = 0;

        return currentConfiguration;
    }
}
