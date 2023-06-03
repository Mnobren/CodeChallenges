using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public static GameBehaviour instance = null;

    [SerializeField] private GameObject eKey;
    private GameObject cue;

    private float currentMoney = 999f;

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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        cue = player.GetComponent<CharacterBehaviour>().GetCueDisplay();
        cue.GetComponentInParent<Canvas>().gameObject.SetActive(false);
        eKey.SetActive(false);
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
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
        }
    }

    public void SpendMoney(float value)
    {
        if(value <= currentMoney) { currentMoney -= value; }
        else { ShowCue("Insufficient money", 2f); }
    }

    public void ShowCue(string message, float seconds)
    {
        CancelInvoke();
        cue.GetComponentInChildren<Text>().text = message;
        cue.GetComponentInParent<Canvas>(true).gameObject.SetActive(true);
        Invoke("HideCue", seconds);
    }

    public void HideCue(string message)
    {
        if(cue.GetComponentInChildren<Text>().text == message)
        {
            CancelInvoke();
            cue.GetComponentInParent<Canvas>().gameObject.SetActive(false);
        }
    }

    public void ShowInteraction(string verb)
    {
        eKey.GetComponentInChildren<Text>(true).text = verb;
        eKey.SetActive(true);
    }

    public void HideInteraction(string verb)
    {
        if(eKey.GetComponentInChildren<Text>(true).text == verb)
        {
            eKey.SetActive(false);
        }
    }
}
