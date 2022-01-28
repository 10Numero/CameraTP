using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{

    public Curve(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float sphereRadius)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
        this.sphereRadius = sphereRadius;
    }

    public Vector3 a;
    public Vector3 b;
    public Vector3 c;
    public Vector3 d;
    public float sphereRadius;

    public Vector3 GetPosition(float t)
    {
        return default;
    }

    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix) 
    {
        var pWorld = transform.localToWorldMatrix.MultiplyPoint(transform.localPosition);
        return pWorld;
    }

    public void DrawGizmo(Color color, Vector3 worldPos)
    {
        Gizmos.DrawSphere(worldPos + a, sphereRadius);
        Gizmos.DrawSphere(worldPos + b, sphereRadius);
        Gizmos.DrawSphere(worldPos + c, sphereRadius);
        Gizmos.DrawSphere(worldPos + d, sphereRadius);
    }
}
