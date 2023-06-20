using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawnerBehaviour : MonoBehaviour
{
    [SerializeField] GameObject ingredient;

    private GameObject sample;

    void Awake()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = ingredient.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    void Start()
    {
        sample = Instantiate(ingredient, transform.position, Quaternion.identity, gameObject.transform);

        sample.GetComponent<DraggableBehaviour>().SubscribeToDropEvent(SampleFit);
    }

    private void SampleFit()
    {
        Transform slot = StageBehaviour.instance.GetSlot();
        if(sample.GetComponent<BoxCollider2D>().bounds.Intersects(slot.GetComponent<BoxCollider2D>().bounds)
            || sample.GetComponent<BoxCollider2D>().bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            sample.GetComponent<DraggableBehaviour>().enabled = false;
            StageBehaviour.instance.FillSlot(sample);
        }
        else
        {
            sample.transform.localPosition = Vector3.zero;
        }
    }
}
