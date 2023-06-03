using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject apparel;

    public float value;

    private string price;
    private bool checking = false;

    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        price = value.ToString()+" $";
    }

    void Update()
    {
        if(checking)
        {
            if(Input.GetButtonDown("Interact"))
            {
                GameObject app = Instantiate(apparel);
                ApparelBehaviour behaviour = app.GetComponent<ApparelBehaviour>();
                behaviour.SetValue(value);
                player.GetComponent<CharacterBehaviour>().EquipApparel( app,
                                                                        behaviour.UpdatePosition,
                                                                        behaviour.UpdateAnimation);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            GameBehaviour.instance.ShowCue(price, 30f);
            GameBehaviour.instance.ShowInteraction("Try");
            checking = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            GameBehaviour.instance.HideCue(price);
            GameBehaviour.instance.HideInteraction("Try");
            checking = false;
        }
    }
}
