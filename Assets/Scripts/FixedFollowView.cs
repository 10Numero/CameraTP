using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    [Header("View Settings")]
    public float roll;
    public float fov;

    [Header("Look At")]
    public Transform target;

    [Header("Constraint")]
    public Transform centralPoint;
    public float yawOffsetMax;
    public float pitchOffsetMax;
    //private Vector3 dir;

    public override CameraConfiguration GetConfiguration()
    {
        #region deprecated
        //dir = target.position - CameraController.Instance.currentCamera.transform.position;
        //var debug = Mathf.Atan2(dir.normalized.x, dir.normalized.z) * Mathf.Rad2Deg;

        //currentConfiguration.yaw = debug;
        //currentConfiguration.pitch = -Mathf.Asin(dir.normalized.y) * Mathf.Rad2Deg;
        #endregion

        var dirTarget = target.position - CameraController.Instance.currentCamera.transform.position;
        var dirCp = centralPoint.position - transform.position;

        var cpAnglePitch = Mathf.Atan2(dirCp.x, dirCp.z) * Mathf.Rad2Deg;
        var dirAnglePitch = Mathf.Atan2(dirTarget.x, dirTarget.z) * Mathf.Rad2Deg;

        var cpAngleYaw = Mathf.Asin(dirCp.y) * Mathf.Rad2Deg;
        float dirAngleYaw = -Mathf.Asin(dirTarget.y) * Mathf.Rad2Deg;



        Quaternion rot = Quaternion.identity;

        if (dirTarget != Vector3.zero)
            rot = Quaternion.LookRotation(dirTarget);

        //Debug.Log("x " + cpAngle + " pitchmax " + pitchOffsetMax);
        //Debug.Log("angle : " + cpAngleYaw);

        ////currentConfiguration.yaw = Mathf.Clamp(float.IsNaN(dirAngleYaw) ? 0 : dirAngleYaw, (-yawOffsetMax + (cpAngleYaw - dirAngleYaw)), yawOffsetMax + (cpAngleYaw - dirAngleYaw));
        //currentConfiguration.pitch = Mathf.Clamp(float.IsNaN(dirAnglePitch) ? 0 : dirAnglePitch, (-pitchOffsetMax + (cpAnglePitch - dirAnglePitch)), pitchOffsetMax + (cpAnglePitch - dirAnglePitch));
        currentConfiguration.roll = roll;
        currentConfiguration.fov = fov;
        currentConfiguration.yaw = rot.eulerAngles.x;
        currentConfiguration.pitch = rot.eulerAngles.y;

        //Debug.Log("ea : " + rot.eulerAngles.x);

        //if (Mathf.Abs((cpAngleYaw - dirAngleYaw)) > yawOffsetMax)
        //    currentConfiguration.yaw = cpAngleYaw - dirAngleYaw > 0 ? yawOffsetMax : -yawOffsetMax;
        ////rot.eulerAngles.Set(xAngle > 0 ? pitchOffsetMax : -pitchOffsetMax, rot.eulerAngles.y, rot.eulerAngles.z);

        //if (Mathf.Abs((cpAnglePitch - dirAnglePitch)) > pitchOffsetMax)
        //    currentConfiguration.pitch = cpAnglePitch - dirAnglePitch > 0 ? pitchOffsetMax : -pitchOffsetMax;

        //if (Mathf.Abs(rot.eulerAngles.y) > yawOffsetMax)
        //    rot.eulerAngles.Set(rot.eulerAngles.x, rot.eulerAngles.y > 0 ? yawOffsetMax : -yawOffsetMax, rot.eulerAngles.z);

        return currentConfiguration;
    }
}
