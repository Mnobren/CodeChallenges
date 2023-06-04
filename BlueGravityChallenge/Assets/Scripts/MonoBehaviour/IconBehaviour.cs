using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconBehaviour : MonoBehaviour
{
    private GameObject model;
    private Color color;
    private Sprite image;

    private GameObject player;

    public void Start()
    {
        //Find player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetUp(GameObject model, Color color, Sprite image)
    {
        //Setup attributes
        this.model = model;
        this.color = color;
        this.image = image;

        //Setup component attributes
        gameObject.GetComponent<Image>().sprite = image;
        gameObject.GetComponent<Image>().color = color;
    }

    public void Click()
    {
        //If icon is clicked then equip corresponding apparel and remove it from inventory
        ApparelBehaviour behaviour = model.GetComponent<ApparelBehaviour>();
        player.GetComponent<CharacterBehaviour>().EquipApparel( model,
                                                                behaviour.UpdatePosition,
                                                                behaviour.UpdateAnimation);
        GameBehaviour.instance.MoveToWorld(model);

        //Close and Open window to refresh
        GameBehaviour.instance.AlternateWindow();
        GameBehaviour.instance.AlternateWindow();
        //Destroy icon
        Destroy(gameObject);
    }
}
