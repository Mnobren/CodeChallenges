using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{
    private int action;
    private Vector3 move;
    private float speed = 200;

    void Start()
    {
        SetAction();
    }
    
    void SetAction()
    {
        action = Random.Range(0, 2);//Random.Range(int, int) is min inclusive and max exclusive.
        if(action == 0) { Walk(); }
        if(action == 1) { Stop(); }
    }

    void Walk()
    {
        int dir = Random.Range(0,4);
        Vector3 v = Vector3.zero;
        if(dir == 0) { v = Vector3.up; }
        else if(dir == 1) { v = Vector3.left; }
        else if(dir == 2) { v = Vector3.down; }
        else if(dir == 3) { v = Vector3.right; }
        move = v * speed;
        gameObject.GetComponent<Rigidbody2D>().velocity = move*Time.deltaTime;
        Invoke("SetAction", Random.Range(10,150)/100);
    }

    void Stop()
    {
        move = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().velocity = move*Time.deltaTime;
        Invoke("SetAction", Random.Range(10,150)/100);
    }
}
