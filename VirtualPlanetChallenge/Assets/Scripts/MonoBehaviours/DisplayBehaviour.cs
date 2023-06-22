using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBehaviour : MonoBehaviour
{
    /*  This script turns this game object
        in a display. Requires a Text object
        in a child object
    */

    private Text component;

    void Start() //Set variables up
    {
        component = GetComponentInChildren<Text>(true); //Fetch text component
        component.text = ""; //Erase previous test
    }

    public void ShowText(string text)
    {
        component.text = text; //Show specified text
    }
}
