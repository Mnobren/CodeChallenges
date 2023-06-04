using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionistBehaviour : MonoBehaviour
{
    private bool checking = false;

    private GameObject player;
    
    void Awake()
    {
        //Find player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //When player is close enough
        if(checking)
        {
            //And E key is pressed
            if(Input.GetButtonDown("Interact"))
            {
                //Calculate price of any apparel not owned
                GameObject[] equip = GameBehaviour.instance.GetEquipped();
                float debt = 0;
                foreach(GameObject app in equip)
                {
                    if(app != null && !app.GetComponent<ApparelBehaviour>().IsAcquired()) { debt += app.GetComponent<ApparelBehaviour>().GetValue(); }
                }

                //Deduct price from player funds
                GameBehaviour.instance.SpendMoney(debt);
                foreach(GameObject app in equip)
                {
                    if(app != null && !app.GetComponent<ApparelBehaviour>().IsAcquired()) { app.GetComponent<ApparelBehaviour>().SetAcquired(true); }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Show tutorial when player is close enough
        if(other.gameObject == player)
        {
            GameBehaviour.instance.ShowCue("Checkout", 30f);
            GameBehaviour.instance.ShowInteraction("Pay");
            checking = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Hide tutorial when player is too far
        if(other.gameObject == player)
        {
            GameBehaviour.instance.HideCue("Checkout");
            GameBehaviour.instance.HideInteraction("Pay");
            checking = false;
        }
    }
}
