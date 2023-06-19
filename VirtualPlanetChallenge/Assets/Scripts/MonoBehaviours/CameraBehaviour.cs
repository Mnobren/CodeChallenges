using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject secondarycam;
    float width = 1280;
    float height = 720;

    void Awake()
    {
        GameObject sec = Instantiate(secondarycam, transform.position, Quaternion.identity);

        var cameraData = sec.GetComponent<Camera>().GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(GetComponent<Camera>());

        float variance = (width/height) / Camera.main.aspect;
        if (variance < 1f)
        {
            gameObject.GetComponent<Camera>().rect = new Rect ((1f - variance) / 2f, 0 , variance, 1f);
        }
    }

    void Start()
    {
    }
}
