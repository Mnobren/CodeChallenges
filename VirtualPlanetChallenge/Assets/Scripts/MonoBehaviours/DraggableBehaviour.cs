using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBehaviour : MonoBehaviour
{
    private RectTransform rect;
    private Vector3 last;
    private Camera cam;

    private bool drag;

    public delegate void ArgLess();

    private event ArgLess GrabEvent;
    private event ArgLess DropEvent;

    void Awake() 
    {
        cam = GameObject.FindGameObjectWithTag("FocusCamera").GetComponent<Camera>();
        last = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
    }
 
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(drag) { RiseDropEvent(); }
            drag = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click =>");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            if (Physics2D.Raycast(ray.origin, ray.direction, new ContactFilter2D().NoFilter(), hits, 20) > 0)
            {
                foreach(RaycastHit2D hit in hits)
                {
                    Debug.Log(hit.transform.name);
                    if (hit.transform.gameObject == gameObject)
                    {
                        drag = true;
                        RiseGrabEvent();
                        Debug.DrawLine(ray.origin, ray.origin+(ray.direction)*10f, Color.green, 5f);
                        Debug.Log("Drag = true");
                        return;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        var current = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        if(drag)
        {
            rect = GetComponent<RectTransform>();
            var delta = (Vector2)(cam.ViewportToWorldPoint(new Vector3(current.x, current.y, 10f)) - cam.ViewportToWorldPoint(new Vector3(last.x, last.y, 10f)));
            rect.anchoredPosition += delta;
        }
        last = current;
    }


    //Event

    private void RiseGrabEvent()
    {
        GrabEvent();
    }
    private void RiseDropEvent()
    {
        DropEvent();
    }

    public void SubscribeToGrabEvent(ArgLess method)
    {
        GrabEvent += method;
    }
    public void SubscribeToDropEvent(ArgLess method)
    {
        DropEvent += method;
    }

    public void UnsubscribeToGrabEvent(ArgLess method)
    {
        GrabEvent -= method;
    }
    public void UnsubscribeToDropEvent(ArgLess method)
    {
        DropEvent -= method;
    }
}
