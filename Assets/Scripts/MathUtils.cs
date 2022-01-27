using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static Vector3 GetNearestPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        Vector3 AC = target - a;
        Vector3 AB = b - a;

        float scalar = Mathf.Clamp(Vector3.Dot(AB.normalized, AC), 0, Vector3.Distance(a, b));

        Vector3 projC = a + AB.normalized * scalar;

        return projC;
    }

    public static Vector3 LinearBezier(Vector3 A, Vector3 B, float t) { return default; }
    public static Vector3 QuadraticBezier(Vector3 A, Vector3 B, Vector3 C, float t) { return default; }
    public static Vector3 CubicBezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t) 
    {
        Vector3 lerpAB = Vector3.Lerp(A, B, t);
        Vector3 lerpBC = Vector3.Lerp(B, C, t);
        Vector3 lerpCD = Vector3.Lerp(C, D, t);
        Vector3 lerpAC = Vector3.Lerp(lerpAB, lerpBC, t);
        Vector3 lerpBD = Vector3.Lerp(lerpBC, lerpCD, t);
        Vector3 finalLerp = Vector3.Lerp(lerpAC, lerpBD, t);
        return finalLerp;
    }
}
