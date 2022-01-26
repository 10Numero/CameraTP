using UnityEngine;

public class CameraPositionDebug : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        if (transform.parent)
                transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = transform.parent.name;
    }
}
