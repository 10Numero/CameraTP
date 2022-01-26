using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CameraController : MonoBehaviour
{
    public Camera currentCamera;
    private CameraConfiguration currentConfiguration;

    private float t;
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
        //currentConfiguration = __config;
    }

    public void AddView(AView view)
    {
        t = 0;
        activeViews.Add(view);
    }

    public void RemoveView(AView view)
    {
        t = 0;
        activeViews.Remove(view);
    }

    #region monobehaviour
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //(o_o)Q�__|___Q(-_-)
        //(-_-)Q_�_|___Q(-_-)
        //(-_-)Q__�|___Q(-_-)
        //(-_-)Q___|�__Q(-_-)
        //(-_-)Q___|_�_Q(-_-)
        //(-_-)Q___|__�Q(o_o)
        //(-_-)Q___|_�_Q(-_-)
        //(-_-)Q___|�__Q(-_-)
        //(-_-)Q__�|___Q(-_-)
        //(-_-)Q_�_|___Q(-_-)
        //(o_o)Q�__|___Q(-_-)
        //(-_-)Q_�_|___Q(-_-)
        //(-_-)Q__�|___Q(-_-)
        //(-_-)Q___|�__Q(-_-)
        //(-_-)Q___|_�_Q(-_-)
        //(-_-)Q___|__�Q(o_o)
        //(-_-)Q___|_�_Q(-_-)
        //(-_-)Q___|�__Q(-_-)
        //(-_-)Q__�|___Q(-_-)
        //(-_-)Q_�_|___Q(-_-)
        //(o_o)Q�__|___Q(-_-)
        //(-_-)Q_�_|___Q(-_-)
        //(-_-)Q__�|___Q(-_-)
        //(-_-)Q___|�__Q(-_-)
        //(-_-)Q___|_�_Q(-_-)
        //(-_-)Q___|__�Q(o_o)
        //(-_-)Q___|_�_Q(-_-)
        //(-_-)Q___|�__Q(-_-)
        //(-_-)Q__�|___Q(-_-)
        //(-_-)Q_�_|___Q(-_-)
        //(o_o)Q�__|___Q(-_-)
        //(-_-)Q_�_|___Q(-_-)
        //(-_-)Q__�|___Q(-_-)
        //(-_-)Q___|�__Q(-_-)
        //(-_-)Q___|_�_Q(-_-)
        //(-_-)Q___|__�Q(o_o)
        //(-_-)Q___|_�_Q(-_-)
        //(-_-)Q___|�__Q(-_-)
        //(-_-)Q__�|___Q(-_-)
        //(-_-)Q_�_|___Q(-_-)
        //(o_o)Q�__|___Q(-_-)
        //(-_-)Q_�_|___Q(-_-)
        //(-_-)Q__�|___Q(-_-)
        //(-_-)Q___|�__Q(-_-)
        //(-_-)Q___|_�_Q(-_-)
        //(-_-)Q___|__�Q(o_o)
        //(-_-)Q___|_�_Q(-_-)
        //(-_-)Q___|�__Q(-_-)
        //(-_-)Q__�|___Q(-_-)
        //(-_-)Q_�_|___Q(-_-)
        //(o_o)Q�__|___Q(-_-)
    }

    private void Update()
    {
        float totalPitch = 0.0f;
        float totalRoll = 0.0f;
        float totalFov = 0.0f;
        float totalWeight = 0.0f;
        Vector3 totalPos = Vector3.zero;

        foreach (var view in activeViews)
        {
            totalFov += view.GetConfiguration().fov * view.weight;
            totalPitch += view.GetConfiguration().pitch * view.weight;
            totalRoll += view.GetConfiguration().roll * view.weight;
            totalWeight += view.weight;
            totalPos += view.GetConfiguration().GetPosition() * view.weight;
        }

        t += speed * Time.deltaTime;
        var funk = EasingFunction.GetEasingFunction(EasingFunction.Ease.EaseInOutQuad);
        float value = funk(0, 1, Mathf.Clamp01(t));


        Quaternion targetRot = Quaternion.Euler(ComputeAverageYaw(), totalPitch / totalWeight, totalRoll / totalWeight);
        Vector3 targetPos = totalPos / totalWeight;

        currentCamera.fieldOfView = Mathf.Lerp(currentCamera.fieldOfView, totalFov / totalWeight, Mathf.Clamp01(value));
        currentCamera.transform.rotation = Quaternion.Lerp(currentCamera.transform.rotation, targetRot, Mathf.Clamp01(value));
        currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, targetPos, Mathf.Clamp01(value));
        
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
