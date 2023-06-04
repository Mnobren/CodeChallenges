using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBehaviour : MonoBehaviour
{
    [SerializeField]private GameObject icon;

    [SerializeField]private GameObject inventoryHolder;
    [SerializeField]private GameObject equippedHolder;

    private GameObject[] inventoryOnDisplay;
    private GameObject[] equippedOnDisplay;
    private GameObject[] inventorySlots;
    private GameObject[] equippedSlots;

    void Awake()
    {
        //Setup array attributtes
        inventoryOnDisplay = new GameObject[9];
        equippedOnDisplay = new GameObject[3];
        inventorySlots = new GameObject[9];
        equippedSlots = new GameObject[3];
    }

    void Start()
    {
        //Setup array with inventory slots
        int x = 0;
        foreach(Transform slot in inventoryHolder.transform)
        {
            inventorySlots[x] = slot.gameObject;
            x++;
        }

        //Setup array with slots of equipped apparel
        foreach(Transform slot in equippedHolder.transform)
        {
            for(int i = 0; i < 3; i++)
            {
                if(slot.gameObject.name == "Equipped"+i) { equippedSlots[i] = slot.gameObject; }
            }
        }

        //Self reference to GameBehaviour
        GameBehaviour.instance.SetWindow(this);
        //Hide window as soon as possible
        gameObject.SetActive(false);
    }

    public void UpdateEquipped(GameObject[] equipped)
    {
        //Manage icons of equipped apparel
        int i = 0;
        foreach(GameObject app in equipped)
        {
            //As long as slot is occupied
            if(app != null)
            {
                //Setup icon
                equippedOnDisplay[i] = equipped[i];
                equippedSlots[i].GetComponentInChildren<IconBehaviour>().SetUp( equipped[i],
                                                                                equipped[i].GetComponent<ApparelBehaviour>().GetColor(),
                                                                                equipped[i].GetComponent<ApparelBehaviour>().GetIcon());
            }
            i++;
        }
    }

    public void OpenWindow()
    {
        PopulateWindow();
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        ClearWindow();
        gameObject.SetActive(false);
    }

    public void PopulateWindow()
    {
        //Manage icons of stored apparel
        List<GameObject> inventory = GameBehaviour.instance.GetInventory();
        int i = 0;
        foreach(GameObject app in inventory)
        {
            //As long as slot is occupied
            if(app != null)
            {
                //Spawn and setup icon
                GameObject button = Instantiate(icon, inventorySlots[i].transform.position, Quaternion.identity, inventorySlots[i].transform);
                inventoryOnDisplay[i] = button;
                button.GetComponent<IconBehaviour>().SetUp( app,
                                                            app.GetComponent<ApparelBehaviour>().GetColor(),
                                                            app.GetComponent<ApparelBehaviour>().GetIcon());
                i++;
            }
        }
    }

    public void ClearWindow()
    {
        //Clear icons to refresh
        for(int i = 0; i < inventoryOnDisplay.Length; i++)
        {
            Destroy(inventoryOnDisplay[i]);
            inventoryOnDisplay[i] = null;
        }
    }
}
