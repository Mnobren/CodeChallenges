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
        //If player presses Q open/close inventory
        if(Input.GetButtonDown("Inventory")) { AlternateWindow(); }
    }

    public void ChangeScene(string name)
    {
        //Load scene with name
        SceneManager.LoadScene(name);
    }

    public bool SpendMoney(float value)
    {
        //If money can be spent update the display
        if(value <= currentMoney)
        {
            currentMoney -= value;
            UpdateMoneyDisplay();
            return true;
        }
        else//Else, show error message
        {
            ShowCue("Insufficient money", 2f);
            return false;
        }
    }

    public void UpdateMoneyDisplay()
    {
        //Format float into string
        moneyDisplay.text = currentMoney.ToString()+" $";
    }

    //"Remove" apparel from world and add to inventory
    public void MoveToInv(GameObject apparel)
    {
        inventory.Add(apparel);
        apparel.SetActive(false);
    }

    //Remove apparel from inventory and "add" to world
    public void MoveToWorld(GameObject apparel)
    {
        apparel.SetActive(true);
        inventory.Remove(apparel);
    }


    //UI//

    //Alternate window between open and closed
    public void AlternateWindow()
    {
        if(!window.gameObject.activeSelf) { window.OpenWindow(); }
        else if(window.gameObject.activeSelf) { window.CloseWindow(); }
    }

    //Show cue for seconds
    public void ShowCue(string message, float seconds)
    {
        CancelInvoke();
        cue.GetComponentInChildren<Text>().text = message;
        cue.GetComponentInParent<Canvas>(true).gameObject.SetActive(true);
        //Automatically hide cue after seconds
        Invoke("HideCue", seconds);
    }

    //Hide cue early or after seconds passed
    public void HideCue(string message)
    {
        if(cue.GetComponentInChildren<Text>().text == message)
        {
            CancelInvoke();
            cue.GetComponentInParent<Canvas>().gameObject.SetActive(false);
        }
    }

    //Show tutorial UI
    public void ShowInteraction(string verb)
    {
        eKey.GetComponentInChildren<Text>(true).text = verb;
        eKey.SetActive(true);
    }

    //Hide tutorial UI
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
