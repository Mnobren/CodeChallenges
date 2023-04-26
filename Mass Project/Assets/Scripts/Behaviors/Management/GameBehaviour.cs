using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    //This script manages the game as a whole, that is mostly scene
    //transitions and in the future cross-sessions

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

    public void Start()
    {
    }

    void OnEnable()
    {//Adds to sceneLoaded event a delegate pointing to LevelJustLoaded
        SceneManager.sceneLoaded += LevelJustLoaded;
    }

    void OnDisable()
    {//Subtracts from sceneLoaded event a delegate pointing to LevelJustLoaded
        SceneManager.sceneLoaded -= LevelJustLoaded;
    }

    void LevelJustLoaded(Scene scene, LoadSceneMode mode)
    {
    }

    public void ChangeScene(string name)
    {
        //Loads a new scene by name
        SceneManager.LoadScene(name);
    }

    public void Destroy(GameObject go)
    {
        //Destroys any GameObject
        MonoBehaviour.Destroy(go);
    }

    public void QuitGame()
    {
        //Quit the game
        Application.Quit();
    }

    //Animation Events
    public void SuccessfulDeploy()
    {
        //After Robot part is in place calls StageBehaviour to proceed with calculations
        StageBehaviour.instance.ConfirmDeploy();
    }
    public void ResolutionCorrect()
    {
        //Spawns the launched robot in the battlefield
        StageBehaviour.instance.ReleaseRobot();
        //Prepares to Generate a new robot and question
        if(StageBehaviour.instance.isPlaying()) { StageBehaviour.instance.StartGame(); }
    }
    public void ResolutionLight()
    {
        //Prepares to Generate a new robot and question
        if(StageBehaviour.instance.isPlaying()) { StageBehaviour.instance.StartGame(); }
    }
    public void ResolutionHeavy()
    {
        //Prepares to Generate a new robot and question
        if(StageBehaviour.instance.isPlaying()) { StageBehaviour.instance.StartGame(); }
    }
}
