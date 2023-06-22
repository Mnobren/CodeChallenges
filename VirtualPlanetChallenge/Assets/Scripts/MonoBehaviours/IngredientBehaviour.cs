using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBehaviour : MonoBehaviour
{
    /*  This script stores important data about
        ingredient instances
    */

    [SerializeField] private int id;
    private string name;

    public void Awake() { name = gameObject.name.Replace("(Clone)", ""); }

    public int ID()
    {
        return id;
    }
    public string Name()
    {
        return name;
    }
}
