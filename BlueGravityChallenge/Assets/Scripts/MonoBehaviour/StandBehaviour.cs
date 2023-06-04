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

/*///////////////////////////
    void Start()
    {
        SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        sprite.sprite = apparel.GetComponent<SpriteRenderer>().sprite;

    }
////////////////////////////*/
    void Update()
    {
        if(checking)
        {
            if(Input.GetButtonDown("Interact"))
            {
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
