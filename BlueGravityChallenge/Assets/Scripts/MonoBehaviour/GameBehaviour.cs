using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public static GameBehaviour instance = null;

    [SerializeField] private GameObject eKey;
    [SerializeField] private Text moneyDisplay;

    private WindowBehaviour window;
    private GameObject cue;

    private List<GameObject> inventory;
    private GameObject[] equipped;

    private float currentMoney = 813f;

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
        //Find Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //Setup UI
        cue = player.GetComponent<CharacterBehaviour>().GetCueDisplay();
        cue.GetComponentInParent<Canvas>().gameObject.SetActive(false);
        eKey.SetActive(false);

        //Setup inventory
        equipped = new GameObject[3];
        inventory = new List<GameObject>();
    }

    public void Update()
    {
        if(Input.GetButtonDown("Inventory")) { AlternateWindow(); }
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

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public bool SpendMoney(float value)
    {
        if(value <= currentMoney)
        {
            currentMoney -= value;
            UpdateMoneyDisplay();
            return true;
        }
        else
        {
            ShowCue("Insufficient money", 2f);
            return false;
        }
    }

    public void UpdateMoneyDisplay()
    {
        moneyDisplay.text = currentMoney.ToString()+" $";
    }

    public void MoveToInv(GameObject apparel)
    {
        inventory.Add(apparel);
        apparel.SetActive(false);
    }

    public void MoveToWorld(GameObject apparel)
    {
        apparel.SetActive(true);
        inventory.Remove(apparel);
    }


    //UI//

    public void AlternateWindow()
    {
        if(!window.gameObject.activeSelf) { window.OpenWindow(); }
        else if(window.gameObject.activeSelf) { window.CloseWindow(); }
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

    //


    //GetSet//

    public float GetCurrentMoney()
    {
        return currentMoney;
    }
    public List<GameObject> GetInventory()
    {
        return inventory;
    }
    public GameObject[] GetEquipped()
    {
        return equipped;
    }
    public void SetEquipped(GameObject[] equipped)
    {
        this.equipped = equipped;

        //Manage Icons
        window.UpdateEquipped(GameBehaviour.instance.GetEquipped());
    }
    public void SetWindow(WindowBehaviour window)
    {
        this.window = window;
    }

    //
}
