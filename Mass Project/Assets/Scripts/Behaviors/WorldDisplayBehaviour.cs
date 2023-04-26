using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDisplayBehaviour : MonoBehaviour
{
    private GameObject targetObject;
    private Vector3 offset;

    void Update()
    {
        if(targetObject != null)
        {
            gameObject.transform.position = targetObject.transform.position + offset;
        }
    }

    //Get/Set
    public void SetTargetObject(GameObject targetObject)
    {
        this.targetObject = targetObject;
    }

    public void SetOffset(Vector3 offset)
    {
        this.offset = offset;
    }
}
