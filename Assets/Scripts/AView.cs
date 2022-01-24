using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    public CameraConfiguration currentConfiguration;
    public float weight;
    public bool isActiveOnStart;

    private void Start()
    {
        SetActive(isActiveOnStart);
    }

    public void SetActive(bool isActive)
    {
        CameraController.Instance.AddView(this);
    }

    protected virtual CameraConfiguration GetConfiguration()
    {
        return currentConfiguration;
    }

    private void OnDisable()
    {
        if(isActiveOnStart) CameraController.Instance.RemoveView(this);
    }
}
