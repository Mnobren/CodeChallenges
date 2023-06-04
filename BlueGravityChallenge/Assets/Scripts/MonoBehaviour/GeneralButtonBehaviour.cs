using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralButtonBehaviour : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        //Load scene with name
        SceneManager.LoadScene(name);
    }
}
