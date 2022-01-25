using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    public float roll;
    public float fov;
    public Transform target;
    Vector3 dir;

    public override CameraConfiguration GetConfiguration()
    {
        //dir = target.position - CameraController.Instance.currentCamera.transform.position;
        //var debug = Mathf.Atan2(dir.normalized.x, dir.normalized.z) * Mathf.Rad2Deg;
        //Debug.Log("debug : " + debug);
        //currentConfiguration.yaw = debug;
        //Quaternion look at entre target et CameraController.Instance.currentCamera.transform.position;
        //euler.x yaw
            //euler.y pitch
        currentConfiguration.pitch = -Mathf.Asin(dir.normalized.y) * Mathf.Rad2Deg;
        currentConfiguration.roll = roll;
        currentConfiguration.fov = fov;

        return currentConfiguration;
    }
}
