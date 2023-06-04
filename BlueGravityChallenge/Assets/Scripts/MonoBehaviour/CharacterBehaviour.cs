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
        //Setup components
        body = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        //Subscribe the method to animate limbs
        AnimateEvent += AnimateChildObject;
    }

    void Update()
    {
        //If movement is greater than zero then raise event
        if(direction.magnitude > 0) { RaiseMovementEvent(); }

        //If any WASD key was pressed or released then check direction of movement
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

        //Prevent any rotations
        transform.rotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        //Every fixed frame set trigger for animations
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
        //Attach apparel to character
        app.GetComponent<ApparelBehaviour>().SetWearer(transform);
        app.transform.position = transform.position;

        //Manage array of equipped apparel
        SpriteRenderer sprite = app.GetComponent<SpriteRenderer>();
        GameObject[] equipped = GameBehaviour.instance.GetEquipped();
        if(equipped[(sprite.sortingOrder/2)-1] != null)
        {
            UnequipApparel( equipped[(sprite.sortingOrder/2)-1],
                            equipped[(sprite.sortingOrder/2)-1].GetComponent<ApparelBehaviour>().UpdatePosition,
                            equipped[(sprite.sortingOrder/2)-1].GetComponent<ApparelBehaviour>().UpdateAnimation);
        }
        equipped[(sprite.sortingOrder/2)-1] = app;
        GameBehaviour.instance.SetEquipped(equipped);

        //Subscribe apparel to events so the apparel follows charater
        MoveEvent += moveMethod;
        AnimateEvent += animateMethod;
    }

    public void UnequipApparel(GameObject app, ArgLess moveMethod, OneString animateMethod)
    {
        //Unsubscribe from events
        MoveEvent -= moveMethod;
        AnimateEvent -= animateMethod;
        
        //Manage array of equipped apparel
        SpriteRenderer sprite = app.GetComponent<SpriteRenderer>();
        GameObject[] equipped = GameBehaviour.instance.GetEquipped();
        equipped[(sprite.sortingOrder/2)-1] = null;
        GameBehaviour.instance.SetEquipped(equipped);

        //Manage unequipped apparel
        if(app.GetComponent<ApparelBehaviour>().IsAcquired()) { GameBehaviour.instance.MoveToInv(app); }
        else { Destroy(app); }
    }


    //ChildObject Related//

    private void AnimateChildObject(string trigger)
    {
        //Method to animate limbs
        for(int i = 0; i < 4; i++)
        {
            //
            Transform child = transform.GetChild(i);
            if(child.GetComponent<Animator>() != null)
            {
                child.gameObject.GetComponent<Animator>().SetTrigger(trigger);
            }
        }
    }

    public GameObject GetCueDisplay()
    {
        //Return cue current display
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
}
