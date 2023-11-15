using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlBehaviour : MonoBehaviour
{
    public static GameControlBehaviour instance = null;

    List<GameObject> deactivated;

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

        deactivated = new List<GameObject>(1);
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
}