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
}
