using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApparelBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private Transform wearer;

    private Animator animator;

    private float value;
    private Color color;
    private bool acquired = false;

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

    public void SetColor(Color color)
    {
        this.color = color;
        GetComponent<SpriteRenderer>().color = color;
    }
    public Color GetColor()
    {
        return color;
    }
    public void SetValue(float value)
    {
        this.value = value;
    }
    public float GetValue()
    {
        return value;
    }
    public void SetAcquired(bool acquired)
    {
        this.acquired = acquired;
    }
    public bool IsAcquired()
    {
        return acquired;
    }
    public Sprite GetIcon()
    {
        return icon;
    }

    //
}
