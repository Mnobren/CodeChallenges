using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        app.GetComponent<ApparelBehaviour>().SetWearer(transform);
        SpriteRenderer sprite = app.GetComponent<SpriteRenderer>();

        //Manage array of equipped apparel
        GameObject[] equipped = GameBehaviour.instance.GetEquipped();
        if(equipped[(sprite.sortingOrder/2)-1] != null)
        {
            UnequipApparel( equipped[(sprite.sortingOrder/2)-1],
                            equipped[(sprite.sortingOrder/2)-1].GetComponent<ApparelBehaviour>().UpdatePosition,
                            equipped[(sprite.sortingOrder/2)-1].GetComponent<ApparelBehaviour>().UpdateAnimation);
        }
        equipped[(sprite.sortingOrder/2)-1] = app;
        GameBehaviour.instance.SetEquipped(equipped);

        //Manage Event subscriptions
        MoveEvent += moveMethod;
        AnimateEvent += animateMethod;
        app.transform.position = transform.position;
    }

    public void UnequipApparel(GameObject app, ArgLess moveMethod, OneString animateMethod)
    {
        MoveEvent -= moveMethod;
        AnimateEvent -= animateMethod;
        SpriteRenderer sprite = app.GetComponent<SpriteRenderer>();
        
        //Manage array of equipped apparel
        GameObject[] equipped = GameBehaviour.instance.GetEquipped();
        equipped[(sprite.sortingOrder/2)-1] = null;
        GameBehaviour.instance.SetEquipped(equipped);

        //Manage unequepped apparel
        if(app.GetComponent<ApparelBehaviour>().IsAcquired()) { GameBehaviour.instance.MoveToInv(app); }
        else { Destroy(app); }
    }


    //ChildObject Related

    private void AnimateChildObject(string trigger)
    {
        for(int i = 0; i < 4; i++)
        {
            Transform child = transform.GetChild(i);
            if(child.GetComponent<Animator>() != null)
            {
                child.gameObject.GetComponent<Animator>().SetTrigger(trigger);
            }
        }
    }

    public GameObject GetCueDisplay()
    {
        for(int i = 0; i < 4; i++)
        {
            Transform child = transform.GetChild(i);
            if(child.GetComponent<Canvas>() != null)
            {
                return child.GetComponentInChildren<Image>().gameObject;
            }
        }
        return null;
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


    //GetSet



    //
}
