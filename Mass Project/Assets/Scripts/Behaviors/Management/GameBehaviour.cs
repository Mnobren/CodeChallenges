using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
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

        Screen.SetResolution(1920, 1080, false);
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
        SceneManager.LoadScene(name);
    }

    public void Destroy(GameObject go)
    {
        MonoBehaviour.Destroy(go);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Animation Events
    public void SuccessfulDeploy()
    {
        StageBehaviour.instance.ConfirmDeploy();
    }
    public void ResolutionCorrect()
    {
        StageBehaviour.instance.ReleaseRobot();
        StageBehaviour.instance.StartGame();
    }
    public void ResolutionLight()
    {
        StageBehaviour.instance.StartGame();
    }
    public void ResolutionHeavy()
    {
        StageBehaviour.instance.StartGame();
    }

    /* #region */ //Local Methods

    

    /* #region */

}
