using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{
    //This script controls the robots in the battlefield

    private int action;
    private float speed = 200;

    void Start()
    {
        //Prepares a new action
        SetAction();
    }
    
    void SetAction()
    {
        action = Random.Range(0, 2);//Random.Range(int, int) is min inclusive and max exclusive.
        //Walks or stops at random
        if(action == 0) { Walk(); }
        if(action == 1) { Stop(); }
    }

    void Walk()
    {
        //Four possible directions of movement
        int dir = Random.Range(0,4);
        Vector3 v = Vector3.zero;
        if(dir == 0) { v = Vector3.up; }
        else if(dir == 1) { v = Vector3.left; }
        else if(dir == 2) { v = Vector3.down; }
        else if(dir == 3) { v = Vector3.right; }
        gameObject.GetComponent<Rigidbody2D>().velocity = v * speed*Time.deltaTime;
        //Will prepare a new action on delay
        Invoke("SetAction", Random.Range(10,150)/100);
    }

    void Stop()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //Will prepare a new action on delay
        Invoke("SetAction", Random.Range(10,150)/100);
    }
}
