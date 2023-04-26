using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPiece
{
    //Class created to manage a single instantiated
    //piece of the robot

    private GameObject sprite;
    private float mass;
    private GameObject display;

    public RobotPiece(GameObject sprite, float mass)
    {
        this.sprite = sprite;
        this.mass = mass;
    }

    public void DestroyGameObjects()
    {
        GameBehaviour.instance.Destroy(sprite);
        GameBehaviour.instance.Destroy(display);
    }

    //Get/Set

    public GameObject GetSprite()
    {
        return sprite;
    }
    public float GetMass()
    {
        return mass;
    }
    public Vector3 GetPosition()
    {
        return sprite.transform.position;
    }
    public GameObject GetDisplay()
    {
        return display;
    }

    public void SetDisplay(GameObject display)
    {
        this.display = display;
    }

    //Set on constructor only
    /*
    public void SetSprite(GameObject sprite)
    {
        this.sprite = sprite;
    }
    public void SetOffset(int mass)
    {
        this.mass = mass;
    }
    */
}
