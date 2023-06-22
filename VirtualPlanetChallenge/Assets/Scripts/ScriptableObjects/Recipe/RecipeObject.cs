using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe Object", order = 1)]
public class RecipeObject : ScriptableObject
{
    public string name;

    public GameObject[] ingredient;
}