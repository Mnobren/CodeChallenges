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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetUp(GameObject model, Color color, Sprite image)
    {
        this.model = model;
        this.color = color;
        this.image = image;

        gameObject.GetComponent<Image>().sprite = image;
        gameObject.GetComponent<Image>().color = color;
    }

    public void Click()
    {
        GameBehaviour.instance.MoveToWorld(model);
        ApparelBehaviour behaviour = model.GetComponent<ApparelBehaviour>();
        player.GetComponent<CharacterBehaviour>().EquipApparel( model,
                                                                behaviour.UpdatePosition,
                                                                behaviour.UpdateAnimation);
    }
}
