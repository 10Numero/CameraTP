using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedView : AView
{
    public float yaw;
    public float pitch;
    public float roll;
    public float fov;

    protected override CameraConfiguration GetConfiguration()
    {
        currentConfiguration.pivot = transform.position;
        currentConfiguration.distance = 0;

        return currentConfiguration;
    }
}
