using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBehaviour : MonoBehaviour
{
    /*  This script makes this game object
        draggable
    */
    
    private RectTransform rect;
    private Vector3 last;
    private Camera cam;

    private bool drag;

    public delegate void ArgLess();

    private event ArgLess GrabEvent;
    private event ArgLess DropEvent;

    private int grabGuests = 0;
    private int dropGuests = 0;

    void Awake() //Set variables up
    {
        cam = GameObject.FindGameObjectWithTag("FocusCamera").GetComponent<Camera>();
        last = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
    }
 
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) //If mouse button is released
        {
            if(drag) { RiseDropEvent(); } //and object is being dragged then drop it
            drag = false;
        }
        if (Input.GetMouseButtonDown(0)) //If mouse button is pressed
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Prepares a ray going to mouse
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            if (Physics2D.Raycast(ray.origin, ray.direction, new ContactFilter2D().NoFilter(), hits, 20) > 0)
            {
                foreach(RaycastHit2D hit in hits)
                {
                    if (hit.transform.gameObject == gameObject) //And if ray hits this game object
                    {
                        drag = true;
                        RiseGrabEvent(); //Grab object
                        return;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        var current = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        if(drag) //If this object is being dragged
        {
            rect = GetComponent<RectTransform>();
            var delta = (Vector2)(cam.ViewportToWorldPoint(new Vector3(current.x, current.y, 10f)) - cam.ViewportToWorldPoint(new Vector3(last.x, last.y, 10f)));
            rect.anchoredPosition += delta; //Move it according to mouse
        }
        last = current;
    }


    //Event

    private void RiseGrabEvent()
    {
        if(grabGuests > 0) { GrabEvent(); }
    }
    private void RiseDropEvent()
    {
        if(dropGuests > 0) { DropEvent(); }
    }

    public void SubscribeToGrabEvent(ArgLess method)
    {
        GrabEvent += method;
        grabGuests += 1;
    }
    public void SubscribeToDropEvent(ArgLess method)
    {
        DropEvent += method;
        dropGuests += 1;
    }

    public void UnsubscribeToGrabEvent(ArgLess method)
    {
        GrabEvent -= method;
        dropGuests -= 1;
    }
    public void UnsubscribeToDropEvent(ArgLess method)
    {
        DropEvent -= method;
        dropGuests -= 1;
    }
}
