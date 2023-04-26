using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraBehaviour : MonoBehaviour
{
    //This script manages the main camera and
    //forces the aspect ratio

    public GameObject secondarycam;
    float fixedratio = (1920/1080);
    float width = 1920;
    float height = 1080;

    void Awake()
    {
        //Instantiate second camera to force aspect ratio.
        GameObject second = Instantiate(secondarycam, transform.position, Quaternion.identity);

        //Adjust current camera aspect ratio.
        float variance = (width/height) / Camera.main.aspect;
        if (variance < 1f)
        {
            gameObject.GetComponent<Camera>().rect = new Rect ((1f - variance) / 2f, 0 , variance, 1f);
        }
    }
}
