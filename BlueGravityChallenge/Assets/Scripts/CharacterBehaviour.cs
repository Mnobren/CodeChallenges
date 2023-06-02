using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public int speed;

    private Rigidbody2D body;
    private Animator animator;

    private Vector3 direction;

    public delegate void ArgLess();
    public delegate void OneString(string one);

    public event ArgLess MoveEvent;
    public event OneString AnimateEvent;

    void Awake()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        AnimateEvent += AnimateChildObject;
    }

    void Update()
    {
        if(direction.magnitude > 0) { RaiseMovementEvent(); }

        if( Input.GetButtonDown("Walk Up") || Input.GetButtonDown("Walk Left") || Input.GetButtonDown("Walk Down") || Input.GetButtonDown("Walk Right")
            || Input.GetButtonUp("Walk Up") || Input.GetButtonUp("Walk Left") || Input.GetButtonUp("Walk Down") || Input.GetButtonUp("Walk Right"))
        {
            direction = Vector3.zero;
            if(Input.GetButton("Walk Up")) { direction += Vector3.up; }
            if(Input.GetButton("Walk Left")) { direction += Vector3.left; }
            if(Input.GetButton("Walk Down")) { direction += Vector3.down; }
            if(Input.GetButton("Walk Right")) { direction += Vector3.right; }
            Vector3.Normalize(direction);
        }
    }

    void FixedUpdate()
    {
        body.velocity = direction * speed * Time.fixedDeltaTime;
        if(direction.magnitude > 0)
        {
            animator.SetTrigger("walk");
            RaiseAnimateEvent("walk");
        }
        else
        {
            animator.SetTrigger("stop");
            RaiseAnimateEvent("stop");
        }
    }

    public void EquipApparel(GameObject app, ArgLess moveMethod, OneString animateMethod)
    {
        app.transform.position = transform.position;
        MoveEvent += moveMethod;
        AnimateEvent += animateMethod;
    }

    public void UnequipApparel(GameObject app, ArgLess moveMethod, OneString animateMethod)
    {
        MoveEvent -= moveMethod;
        AnimateEvent -= animateMethod;
        Destroy(app);
    }


    //ChildObject Related

    private void AnimateChildObject(string trigger)
    {
        for(int i = 0; i < 3; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.GetComponent<Animator>().SetTrigger(trigger);
        }
    }

    //

    
    //Event Related//

    private void RaiseMovementEvent()
    {
        if(MoveEvent != null) { MoveEvent(); }
    }

    private void RaiseAnimateEvent(string trigger)
    {
        if(AnimateEvent != null) { AnimateEvent(trigger); }
    }

    //
}
