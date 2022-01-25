using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    protected CameraConfiguration currentConfiguration = new CameraConfiguration();
    [Range(0, 1)]public float weight;
    public bool isActiveOnStart;

    private void Start()
    {
        SetActive(isActiveOnStart);
    }

    public void SetActive(bool isActive)
    {
        if (isActive) CameraController.Instance.AddView(this);
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
