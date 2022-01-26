using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    [Header("Default Settings")]
    [Range(0.000001f, 1)] public float weight;
    protected CameraConfiguration currentConfiguration;
    public bool isActiveOnStart;

    private void Awake()
    {
        currentConfiguration = new CameraConfiguration(this.transform);


        //currentConfiguration = new GameObject("CameraConfiguration " + transform.name).AddComponent<CameraConfiguration>();
        //currentConfiguration.gameObject.transform.SetParent(transform);
        //currentConfiguration.transform.localPosition = Vector3.zero;
    }

    private void Start()
    {
        SetActive(isActiveOnStart);
    }

    public void SetActive(bool isActive)
    {
        if (isActive) CameraController.Instance.AddView(this);
    }

    [Sirenix.OdinInspector.Button]
    private void AddCamera()
    {
        CameraController.Instance.AddView(this);
    }

    [Sirenix.OdinInspector.Button]
    private void RemoveCamera()
    {
        CameraController.Instance.RemoveView(this);
    }

    public virtual CameraConfiguration GetConfiguration()
    {
        return currentConfiguration;
    }

    private void OnDisable()
    {
        if(isActiveOnStart) CameraController.Instance.RemoveView(this);
    }
}
