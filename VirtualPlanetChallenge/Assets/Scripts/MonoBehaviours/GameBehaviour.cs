using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    /*  This script/game object controls everything
        related to the game as a whole
    */

    public static GameBehaviour instance = null;
    
    void Awake()
    {
        //Singleton Pattern (kind of)
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        Application.runInBackground = true; //Set this game to run normally in background
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name); //Load scene by name
    }
}
