using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CameraController : AView
{
    public Camera currentCamera;

    public FixedView a1;
    public FixedView a2;

    public float t;
    public float speed;

    public List<AView> activeViews = new List<AView>();

    public static CameraController Instance;


    public float ComputeAverageYaw()
    {
        Vector2 sum = Vector2.zero;

        foreach (var config in activeViews)
        {
            sum += new Vector2(Mathf.Cos(config.GetConfiguration().yaw * Mathf.Deg2Rad),
            Mathf.Sin(config.GetConfiguration().yaw * Mathf.Deg2Rad)) * config.weight;
        }

        return Vector2.SignedAngle(Vector2.right, sum);
    }

    public void ApplyConfiguration(Camera __camera, CameraConfiguration __config)
    {
        currentConfiguration = __config;
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

    private void Start()
    {
        //(o_o)Q起_|___Q(-_-)
        //(-_-)Q_起|___Q(-_-)
        //(-_-)Q__院___Q(-_-)
        //(-_-)Q___|起_Q(-_-)
        //(-_-)Q___|_起Q(-_-)
        //(-_-)Q___|__討(o_o)
        //(-_-)Q___|_起Q(-_-)
        //(-_-)Q___|起_Q(-_-)
        //(-_-)Q__院___Q(-_-)
        //(-_-)Q_起|___Q(-_-)
        //(o_o)Q起_|___Q(-_-)
        //(-_-)Q_起|___Q(-_-)
        //(-_-)Q__院___Q(-_-)
        //(-_-)Q___|起_Q(-_-)
        //(-_-)Q___|_起Q(-_-)
        //(-_-)Q___|__討(o_o)
        //(-_-)Q___|_起Q(-_-)
        //(-_-)Q___|起_Q(-_-)
        //(-_-)Q__院___Q(-_-)
        //(-_-)Q_起|___Q(-_-)
        //(o_o)Q起_|___Q(-_-)
        //(-_-)Q_起|___Q(-_-)
        //(-_-)Q__院___Q(-_-)
        //(-_-)Q___|起_Q(-_-)
        //(-_-)Q___|_起Q(-_-)
        //(-_-)Q___|__討(o_o)
        //(-_-)Q___|_起Q(-_-)
        //(-_-)Q___|起_Q(-_-)
        //(-_-)Q__院___Q(-_-)
        //(-_-)Q_起|___Q(-_-)
        //(o_o)Q起_|___Q(-_-)
        //(-_-)Q_起|___Q(-_-)
        //(-_-)Q__院___Q(-_-)
        //(-_-)Q___|起_Q(-_-)
        //(-_-)Q___|_起Q(-_-)
        //(-_-)Q___|__討(o_o)
        //(-_-)Q___|_起Q(-_-)
        //(-_-)Q___|起_Q(-_-)
        //(-_-)Q__院___Q(-_-)
        //(-_-)Q_起|___Q(-_-)
        //(o_o)Q起_|___Q(-_-)
        //(-_-)Q_起|___Q(-_-)
        //(-_-)Q__院___Q(-_-)
        //(-_-)Q___|起_Q(-_-)
        //(-_-)Q___|_起Q(-_-)
        //(-_-)Q___|__討(o_o)
        //(-_-)Q___|_起Q(-_-)
        //(-_-)Q___|起_Q(-_-)
        //(-_-)Q__院___Q(-_-)
        //(-_-)Q_起|___Q(-_-)
        //(o_o)Q起_|___Q(-_-)
    }

    private void Update()
    {
        float totalPitch = 0.0f;
        float totalRoll = 0.0f;
        float totalFov = 0.0f;
        float totalWeight = 0.0f;

        foreach (var view in activeViews)
        {
            totalFov += view.GetConfiguration().fov * view.weight;
            totalPitch += view.GetConfiguration().pitch * view.weight;
            totalRoll += view.GetConfiguration().roll * view.weight;
            totalWeight += view.weight;
        }
        t += speed * Time.deltaTime;
        var funk = EasingFunction.GetEasingFunction(EasingFunction.Ease.EaseInOutQuad);
        float value = funk(0, 1, t);
        Quaternion variableAPart = Quaternion.Euler(ComputeAverageYaw(), totalPitch / totalWeight, totalRoll / totalWeight);
        currentCamera.fieldOfView = Mathf.Lerp(currentConfiguration.fov, totalFov / totalWeight, value);
        currentCamera.transform.rotation = Quaternion.Lerp(currentCamera.transform.rotation, variableAPart, value);
        
    }

    //public void DrawGizmos(Color color)
    //{
    //    Gizmos.color = color;
    //    Gizmos.DrawSphere(currentConfiguration.pivot, 0.25f);
    //    Vector3 position = currentConfiguration.GetPosition();
    //    Gizmos.DrawLine(currentConfiguration.pivot, position);
    //    Gizmos.matrix = Matrix4x4.TRS(position, currentConfiguration.GetRotation(), Vector3.one);
    //    Gizmos.DrawFrustum(Vector3.zero, currentConfiguration.fov, 0.5f, 0f, Camera.main.aspect);
    //    Gizmos.matrix = Matrix4x4.identity;
    //}
    #endregion
}
