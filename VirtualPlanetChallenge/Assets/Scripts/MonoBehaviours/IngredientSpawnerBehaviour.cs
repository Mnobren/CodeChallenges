using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawnerBehaviour : MonoBehaviour
{
    /*  This script controls both the ingredient
        spawner and the ingredient itself
    */

    [SerializeField] GameObject ingredient;

    private GameObject sample;

    void Awake() //Set this spawner object sprite equal to ingredient sprite
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = ingredient.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    void Start()
    {
        CreateSample();
    }

    private void CreateSample() //Spawn an intance of ingredient above this spawner object
    {
        sample = Instantiate(ingredient, transform.position, Quaternion.identity, gameObject.transform);
        sample.GetComponent<DraggableBehaviour>().SubscribeToDropEvent(SampleFit); //Listen to spawned ingredient being dropped
    }

    private void SampleFit() //When spawned ingredient is dropped
    {
        Transform slot = StageBehaviour.instance.GetSlot();
        if(sample.GetComponent<BoxCollider2D>().bounds.Intersects(slot.GetComponent<BoxCollider2D>().bounds)//If it is dropped in the slot
            || sample.GetComponent<BoxCollider2D>().bounds.Intersects(GetComponent<BoxCollider2D>().bounds))//or in the spawner
        {
            sample.GetComponent<DraggableBehaviour>().enabled = false; //Stop listening
            StageBehaviour.instance.FillSlot(sample); //Put ingredinet in sandwich
            CreateSample(); //Spawn another instance
        }
        else //If not
        {
            sample.transform.localPosition = Vector3.zero; //Return ingredient to previous position
        }
    }
}
