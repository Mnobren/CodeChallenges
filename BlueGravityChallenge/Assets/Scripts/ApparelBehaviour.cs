using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparelBehaviour : MonoBehaviour
{
    [SerializeField] private Transform wearer;

    private Animator animator;

    private float value;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();//Must be on Start()!
    }

    public void SetWearer(Transform wearer)
    {
        this.wearer = wearer;
    }

    public void UpdatePosition()
    {
        if(gameObject != null) gameObject.transform.position = wearer.position;
    }

    public void UpdateAnimation(string trigger)
    {
        if(animator != null) animator.SetTrigger(trigger);
    }


    //GetSet

    public void SetValue(float value)
    {
        this.value = value;
    }
}
