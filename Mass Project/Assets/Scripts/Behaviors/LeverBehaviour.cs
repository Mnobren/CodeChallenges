using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverBehaviour : MonoBehaviour
{
    //This script create the lever mechanic
    public GameObject LeverSlider;
    public GameObject NumberDisplayText;

    private float current;

    int range = 198;

    public void UpdateNumberDisplay()
    {
        //Convert current number from slider range(0-1) to declared range
        current = (int)(LeverSlider.GetComponent<Slider>().value*range-range/2);
        
        //If current number equals zero or next to it, then label "Standard"
        if(current == 0) { NumberDisplayText.GetComponent<Text>().text = "Padrão"; }
        else { NumberDisplayText.GetComponent<Text>().text = current.ToString("F0"); }
    }

    public float GetCurrent()
    {
        return current;
    }
}
