using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraBehaviour : MonoBehaviour
{
    /*  This script sets up this camera as an
        overlay camera to force a specific aspect
        ratio in the game, regardless of platform
    */

    public GameObject secondarycam;

    private Camera component;
    private float standard;

    float width = 1280;
    float height = 720;

    void Awake()
    {
        GameObject sec = Instantiate(secondarycam, transform.position, Quaternion.identity); //Instantiate a second camera

        //Set second camera as a black base camera
        var cameraData = sec.GetComponent<Camera>().GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(GetComponent<Camera>()); //Set this camera as overlay camera
        float variance = (width/height) / Camera.main.aspect;
        if (variance < 1f)
        {
            gameObject.GetComponent<Camera>().rect = new Rect ((1f - variance) / 2f, 0 , variance, 1f); //Resize this camera to aspect ratio
        }
    }


    //A series of calculations to increase this camera culling range

    void Start()
    {
        component = GetComponent<Camera>();
        standard = component.farClipPlane;
    }

    public void OnEnable()
    {
        Camera.onPreCull += PreCullAdjustFOV;
        Camera.onPreRender += PreRenderAdjustFOV;
    }

    public void OnDisable()
    {
        Camera.onPreCull -= PreCullAdjustFOV;
        Camera.onPreRender -= PreRenderAdjustFOV;
    }

    public void PreCullAdjustFOV(Camera cam)
    {
        if (cam == component)
        {
            standard = component.farClipPlane;
            component.farClipPlane = 100; //Increase vision range before culling
        }
    }

    public void PreRenderAdjustFOV(Camera cam)
    {
        if (cam == component)
        {
            component.farClipPlane = standard; //Restore previous vision range before rendering
        }
    }
}
