using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBehaviour : MonoBehaviour
{
    private Text component;

    void Start()
    {
        component = GetComponentInChildren<Text>();
        component.text = "";
    }

    public void ShowText(string text)
    {
        component.text = text;
    }
}
