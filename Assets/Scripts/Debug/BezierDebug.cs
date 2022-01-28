using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BezierDebug : MonoBehaviour
{
    public Curve curve;
    public Transform sphere;
    [Range(0, 1)] public float rangeT;

    private void Start()
    {
    }

    private void Update()
    {
        sphere.position = MathUtils.CubicBezier(curve.a + transform.position, curve.b + transform.position, curve.c + transform.position, curve.d + transform.position, rangeT);
    }

    private void OnDrawGizmos()
    {
        curve.DrawGizmo(Color.magenta, transform.position);
    }
}
