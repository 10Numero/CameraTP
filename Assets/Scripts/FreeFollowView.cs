using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFollowView : AView
{
    public float pitch;
    public float roll;
    public float fov;
    public float yaw;
    public float yawSpeed;
    public Transform target;
    public Curve curve;
    public float curvePosition;
    public float curveSpeed;

    public void FollowTarget(Vector3 target)
    {
        transform.position = target;
    }

    public void Update()
    {
        yawHorizontalInput();
        curvePositionVerticalInput();
        A();
    }

    public void yawHorizontalInput()
    {
        yaw = Input.GetAxis("Horizontal") * yawSpeed;
    }

    public void curvePositionVerticalInput()
    {
        curvePosition -= Input.GetAxis("Vertical") * curveSpeed * Time.deltaTime;
        curvePosition = Mathf.Clamp01(curvePosition);
    }

    public Matrix4x4 ComputeCurveToWorldMatrix()
    {
        Quaternion rotation = Quaternion.Euler(0, yaw, 0);
        return Matrix4x4.TRS(target.transform.position, rotation, Vector3.one);
    }

    [Sirenix.OdinInspector.Button]
    public void A()
    {
        var elRetourDeLaMatrix = ComputeCurveToWorldMatrix();
        Vector3 elPos = elRetourDeLaMatrix.GetColumn(3);
        Vector3 rot = elRetourDeLaMatrix.GetColumn(2);
        //transform.rotation = Quaternion.Euler(transform.rotation.x, yaw, transform.rotation.z);
        transform.position = 
            MathUtils.CubicBezier(
            curve.a + elPos,
            curve.b + elPos,
            curve.c + elPos,
            curve.d + elPos,
            Mathf.Clamp01(curvePosition));
        transform.position += new Vector3(yaw, 0, 0);
        Debug.Log(elPos);
    }
}
