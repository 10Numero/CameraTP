using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : AView
{
    public Camera currentCamera;

    private List<AView> activeViews = new List<AView>();

    public static CameraController Instance;

    public float ComputeAverageYaw()
    {
        Vector2 sum = Vector2.zero;

        foreach (var config in activeViews)
        {
            sum += new Vector2(Mathf.Cos(config.currentConfiguration.yaw * Mathf.Deg2Rad),
            Mathf.Sin(config.currentConfiguration.yaw * Mathf.Deg2Rad)) * config.weight;
        }

        return Vector2.SignedAngle(Vector2.right, sum);
    }

    public void ApplyConfiguration(Camera __camera, CameraConfiguration __config)
    {
        
    }

    public void AddView(AView view)
    {
        activeViews.Add(view);
    }

    public void RemoveView(AView view)
    {
        activeViews.Remove(view);
    }

    #region monobehaviour
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
    }

    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(currentConfiguration.pivot, 0.25f);
        Vector3 position = currentConfiguration.GetPosition();
        Gizmos.DrawLine(currentConfiguration.pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, currentConfiguration.GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, currentConfiguration.fov, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }
    #endregion
}
