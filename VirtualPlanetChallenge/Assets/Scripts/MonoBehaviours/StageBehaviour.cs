using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StageBehaviour : MonoBehaviour
{
    /*  This script and game object controls everything
        related to this specific stage
    */

    public static StageBehaviour instance = null;

    [SerializeField] private GameObject timeDisplay;
    [SerializeField] private GameObject scoreDisplay;
    [SerializeField] private GameObject resultDisplay;
    [SerializeField] private Transform orderDisplay;
    [SerializeField] private Transform slot;
    [SerializeField] private List<RecipeObject> orders;
    [SerializeField] private Animator visualCue;
    [SerializeField] private PlayableDirector introTimeline;
    [SerializeField] private PlayableDirector endTimeline;

    private const float step = 0.62f;
    private const float duration = 120f;

    private int score;
    private bool countdown = false;
    private DateTime start;
    private DateTime finish;
    private RecipeObject currentOrder;
    private GameObject[] sandwich = new GameObject[3];
    private int counter = 0;
    
    void Awake()
    {
        //There can be only one Stage Manager per scene
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Play Intro as soon as possible
        PlayIntro();
        timeDisplay.SetActive(false);
        scoreDisplay.SetActive(false);
    }

    void FixedUpdate()
    {
        //if game has begun then update countdown display every frame
        if(countdown)
        {
            if(timeDisplay.activeSelf == false) { timeDisplay.SetActive(true); }
            timeDisplay.GetComponent<DisplayBehaviour>().ShowText(finish.Subtract(DateTime.Now).Minutes+":"+finish.Subtract(DateTime.Now).Seconds);
        }
    }

    private void PlayIntro()
    {
        introTimeline.Play(); //Intro will call StartGame when finished
    }

    public void StartGame() //Set everything up to start the game
    {
        score = 0; //Redet score
        countdown = true; //Flag to start countdown
        start = DateTime.Now; //Store start time
        finish = start + new TimeSpan(0,2,0); //Estimate finish time

        if(scoreDisplay.activeSelf == false) { scoreDisplay.SetActive(true); }
        IncrementScore(0); //Call IncrementScore to update the HUD display
        SetNextOrder(); //Choose next order and show it in the UI

        StartCoroutine("TimeOut"); //Start coroutine to count down the time out
    }

    private void SetNextOrder() //Prepare next order/recipe
    {
        currentOrder = orders[UnityEngine.Random.Range(0, orders.Count)];
        foreach(Transform child in orderDisplay) //Set up children objects
        {
            if(child.name == "Label") //Recipe name
            {
                child.GetComponentInChildren<UnityEngine.UI.Text>().text = currentOrder.name;
            }
            else if(child.name == "First") //Recipe first item
            {
                child.GetComponent<SpriteRenderer>().sprite = currentOrder.ingredient[0].GetComponentInChildren<SpriteRenderer>().sprite;
            }
            else if(child.name == "Second") //Recipe second item
            {
                child.GetComponent<SpriteRenderer>().sprite = currentOrder.ingredient[1].GetComponentInChildren<SpriteRenderer>().sprite;
            }
            else if(child.name == "Third") //Recipe third item
            {
                child.GetComponent<SpriteRenderer>().sprite = currentOrder.ingredient[2].GetComponentInChildren<SpriteRenderer>().sprite;
            }
        }
    }

    public void FillSlot(GameObject obj) //Move ingredient to slot position and move slot object to next location
    {
        obj.transform.position = slot.position;
        obj.GetComponentInChildren<SpriteRenderer>().sortingOrder = slot.GetComponent<SpriteRenderer>().sortingOrder;
        if(obj.GetComponent<IngredientBehaviour>().ID() == 0) { Destroy(obj); FinishSandwich(); } //ID 0 is the uppermost ingredient, the bread.
        else { IncrementSandwich(obj); }
        NextSlot();
    }

    private void IncrementSandwich(GameObject obj) //Add ingredient gameobject to sandwich array 
    {
        if(counter == sandwich.Length) //Resize array if necessary
        {
            GameObject[] array = new GameObject[sandwich.Length];
            for(int i = 0; i < array.Length; i++) { array[i] = sandwich[i]; }
            sandwich = new GameObject[counter+1];
            for(int i = 0; i < array.Length; i++) { sandwich[i] = array[i]; }
        }
        sandwich[counter] = obj;
        counter++;
    }

    private void FinishSandwich() //Sandwich is finished, calculate results
    {
        counter = 0;
        for(int i = 0; i < Mathf.Max(sandwich.Length, currentOrder.ingredient.Length); i++)
        {
            //If there are missing or extra ingredients
            if(currentOrder.ingredient[i] == null || sandwich[i] == null)
            {
                ScoreAndRepeat(false); //Sandwich is wrong
                return;
            }
            //If sandwich ingredients don't match recipe
            else if(sandwich[i].GetComponent<IngredientBehaviour>().ID() != currentOrder.ingredient[i].GetComponent<IngredientBehaviour>().ID())
            {
                ScoreAndRepeat(false); //Sandwich is wrong
                return;
            }
        }
        //if this line runs then
        ScoreAndRepeat(true);  //Sandwich is perfect
    }

    private void ScoreAndRepeat(bool correct) //Asjust score and prepare next order
    {
        if(correct) { IncrementScore(3); visualCue.SetTrigger("right");  } //Set corresponding animation trigger
        else { IncrementScore(-3); visualCue.SetTrigger("wrong"); } //Set corresponding animation trigger
        ResetSlots(); //Reset slot object position
        SetNextOrder(); //Prepare next order
    }

    private void ResetSlots() //Reset slot object position and erase ingredients in sandwich
    {
        int aux = 0;
        foreach(GameObject ingredient in sandwich)
        {
            if(ingredient != null)
            {
                aux += 1; //Counts the number of ingredients
                Destroy(ingredient); //Destroy ingredient objects in sandwich
            }
        }
        sandwich = new GameObject[3]; //Reset sandwich array
        aux += 1; //Counts one more for the bread
        slot.position += new Vector3(0, -(aux*step), 0); //Slot object steps back once for each ingredient
        slot.GetComponent<SpriteRenderer>().sortingOrder = 1; // Slot object returns to order 1
    }

    private void IncrementScore(int value) //Add positive or negative value to score
    {
        score += value;
        scoreDisplay.GetComponent<DisplayBehaviour>().ShowText(score+" Points"); //Update display with new score
    }

    private void NextSlot() //Move slot object to next position
    {
        slot.position += new Vector3(0, step, 0);
        slot.GetComponent<SpriteRenderer>().sortingOrder += 1; //Adjust slot sorting order to stay above other ingredients
    }

    private IEnumerator TimeOut() //Start countdown to time out
    {
        yield return new WaitForSecondsRealtime(duration); //Wait <duration> seconds before time out
        countdown = false; //Stop counting down
        resultDisplay.GetComponent<DisplayBehaviour>().ShowText("You scored "+score+" points !"); //Prepare total score screen
        endTimeline.Play(); //Play ending timeline
    }

    //GetSet

    public Transform GetSlot()
    {
        return slot;
    }
}
