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
        //Find player
        player = GameObject.FindGameObjectWithTag("Player");
        //Format price to string
        price = value.ToString()+" $";
    }
    
    void Update()
    {
        //When player is close enough
        if(checking)
        {
            //And E key is pressed
            if(Input.GetButtonDown("Interact"))
            {
                //Player try out the apparel
                GameObject app = Instantiate(apparel);
                ApparelBehaviour behaviour = app.GetComponent<ApparelBehaviour>();
                SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
                behaviour.SetValue(value);
                behaviour.SetColor(sprite.color);
                player.GetComponent<CharacterBehaviour>().EquipApparel( app,
                                                                        behaviour.UpdatePosition,
                                                                        behaviour.UpdateAnimation);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Show tutorial when player is close enough
        if(other.gameObject == player)
        {
            GameBehaviour.instance.ShowCue(price, 30f);
            GameBehaviour.instance.ShowInteraction("Try");
            checking = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Hide tutorial when player is too far
        if(other.gameObject == player)
        {
            GameBehaviour.instance.HideCue(price);
            GameBehaviour.instance.HideInteraction("Try");
            checking = false;
        }
    }
}
