using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionistBehaviour : MonoBehaviour
{
    private bool checking = false;

    private GameObject player;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(checking)
        {
            if(Input.GetButtonDown("Interact"))
            {
                GameObject[] equip = GameBehaviour.instance.GetEquipped();
                float debit = 0;
                foreach(GameObject app in equip)
                {
                    if(app != null && !app.GetComponent<ApparelBehaviour>().IsAcquired()) { debit += app.GetComponent<ApparelBehaviour>().GetValue(); }
                }

                GameBehaviour.instance.SpendMoney(debit);
                foreach(GameObject app in equip)
                {
                    if(app != null && !app.GetComponent<ApparelBehaviour>().IsAcquired()) { app.GetComponent<ApparelBehaviour>().SetAcquired(true); }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            GameBehaviour.instance.ShowCue("Checkout", 30f);
            GameBehaviour.instance.ShowInteraction("Pay");
            checking = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            GameBehaviour.instance.HideCue("Checkout");
            GameBehaviour.instance.HideInteraction("Pay");
            checking = false;
        }
    }
}
