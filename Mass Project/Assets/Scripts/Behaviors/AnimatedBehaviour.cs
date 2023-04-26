using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBehaviour : MonoBehaviour
{
    //This script allows any animated object to call
    //any method on the Game Manager
    
    public void CallMethod(string method)
    {
        Type t = GameBehaviour.instance.GetType();
        MethodInfo info = t.GetMethod(method);
        info.Invoke(GameBehaviour.instance, null);
    } 
}
