using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageBehaviour : MonoBehaviour
{
    public static StageBehaviour instance = null;

    public int stageDifficult = 0;
    public int targetAnswers = 1;
    public GameObject world;
    public GameObject worldDisplay;
    public GameObject battlefield;
    public GameObject robotInAction;
    public GameObject part;
    public GameObject healthSlider;
    public GameObject leverSlider;
    public GameObject machine;
    public GameObject launchButton;
    public GameObject[] boxes;
    public GameObject winScreen;
    public GameObject overScreen;

    private float health = 100f;
    private bool playing = true;
    private int confirmed = 0;
    private int correctAnswers = 0;

    private RobotPiece[] currentPieces;
    private List<GameObject> currentDisplays;

    void Awake()
    {
        //the static instance makes this MonoBehaviour accessible globally
        instance = this;
        //Setting up list
        currentDisplays = new List<GameObject>();
        healthSlider.GetComponent<Slider>().interactable = false;
        //Hide game over screens to show later
        overScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    void OnEnable()
    {//Adds to sceneLoaded event a delegate pointing to LevelJustLoaded
        SceneManager.sceneLoaded += LevelJustLoaded;
    }

    void OnDisable()
    {//Subtracts from sceneLoaded event a delegate pointing to LevelJustLoaded
        SceneManager.sceneLoaded -= LevelJustLoaded;
    }

    void LevelJustLoaded(Scene scene, LoadSceneMode mode)
    {//Start up the scene
        correctAnswers = 0;
        StartGame();
    }

    void Update()
    {
        //while playing health is drained
        if(playing)
        {
            DamageHealth(1f*Time.deltaTime);
        }
    }

    public void StartGame()
    {
        playing = true;
        //Disable answer button
        launchButton.GetComponent<Button>().interactable = false;
        //Set up a new array of pieces
        currentPieces = new RobotPiece[2];
        //Create a new robot to be built
        DeployRobot();
    }

    void DeployRobot()
    {
        if(playing)
        {
            //Declaring array to store mass A and mass B
            float[] expression;
            //Populate array with values according to stage difficult
            GenerateQuestion(out expression);
            //Create RobotPieces
            for(int i = 0; i < 2; i++)
            {
                //DeployPart will instantiate a sprite in the game for the robot piece
                GameObject p = DeployPart(i);
                //Instantiate a new robot piece
                RobotPiece piece = new RobotPiece(p, expression[i]);
                //Add piece to the array
                currentPieces[i] = piece;
            }
        }
    }

    int GenerateQuestion(out float[] expression)
    {
        float a = 0;
        float b = 0;
        //For difficult 0 two digits
        if(stageDifficult == 0)
        {
            a = Random.Range(1, 50);//Unity Random.Range(int, int) is min inclusive max exclusive.
            b = Random.Range(1, 50);
        }
        //For difficult 1 three digits //WARNING: UI for three digits not implemented yet
        else if(stageDifficult == 1)
        {
            a = Random.Range(1, 500);
            b = Random.Range(1, 500);
        }
        //For difficult 2 two digits and negative numbers
        else if(stageDifficult == 2)
        {
            a = Random.Range(-50, 50);
            b = Random.Range(-50, 50);
        }
        //For difficult 3 two digits and negative numbers //WARNING: UI for three digits not implemented yet
        else if(stageDifficult == 3)
        {
            a = Random.Range(-500, 500);
            b = Random.Range(-500, 500);
        }
        expression = new float[2];
        expression[0] = a;
        expression[1] = b;
        return stageDifficult;
    }

    GameObject DeployPart(int b)
    {
        //Select spawn point according to box
        Transform box = boxes[b].transform;
        //Instantiate sprite
        GameObject p = Instantiate(part, box.position, Quaternion.identity, box);
        //Play animation according to spawn point
        if(b == 0) { p.GetComponent<Animator>().SetTrigger("LeftDeploy"); }
        else if(b == 1) { p.GetComponent<Animator>().SetTrigger("RightDeploy"); }

        return p;
    }

    public void ConfirmDeploy()
    {
        //One part of the robot is in place
        confirmed += 1;

        //Both parts are in place, proceed
        if(confirmed == 2 && playing)
        {
            confirmed = 0;
            WaitForInput();
        }
    }

    void WaitForInput()
    {
        //Enable the button so the player can answer
        launchButton.GetComponent<Button>().interactable = true;
        //Spawn displays to show mass of pieces
        int d1 = ShowWorldDisplay(currentPieces[0].GetSprite(), world.transform, currentPieces[0].GetMass().ToString(), new Vector3(0, -0.8f, 0));
        int d2 = ShowWorldDisplay(currentPieces[1].GetSprite(), world.transform, currentPieces[1].GetMass().ToString(), new Vector3(0, -0.8f, 0));
        //Add reference to display on RobotPiece
        currentPieces[0].SetDisplay(currentDisplays[d1]);
        currentPieces[1].SetDisplay(currentDisplays[d2]);
    }

    public void InputAnswer()
    {
        //Clear RobotPieces
        foreach(RobotPiece p in currentPieces) { p.DestroyGameObjects(); }
        //Calculate correct answer
        float correct = currentPieces[0].GetMass() + currentPieces[1].GetMass();
        //Play success or fail animation according to player answer
        if(leverSlider.GetComponent<LeverBehaviour>().GetCurrent() == correct)
        {
            machine.GetComponent<Animator>().SetTrigger("Correct");//Correct answer
        }
        else if (leverSlider.GetComponent<LeverBehaviour>().GetCurrent() < correct)
        {
            machine.GetComponent<Animator>().SetTrigger("Heavier");//The Robot is actually heavier
        }
        else if (leverSlider.GetComponent<LeverBehaviour>().GetCurrent() > correct)
        {
            machine.GetComponent<Animator>().SetTrigger("Lighter");//The Robot is actually lighter
        }
    }

    public void ReleaseRobot()
    {
        //Release the robot in the battlefield
        Instantiate(robotInAction, battlefield.transform.position, Quaternion.identity, world.transform);
        correctAnswers += 1;
        if(correctAnswers >= targetAnswers) { GameWon(); }
    }

    void DamageHealth(float value)
    {
        health -= value;
        //Update Health display
        if (health > 0) { healthSlider.GetComponent<Slider>().value = health/100; }
        else
        {
            healthSlider.GetComponent<Slider>().value = 0;
            //Declare game over
            GameOver();
        }
    }

    void GameWon()
    {
        //Stop game
        playing = false;
        //Show game over screen
        winScreen.SetActive(true);
    }

    void GameOver()
    {
        //Stop game
        playing = false;
        //Show game over screen
        overScreen.SetActive(true);
    }

    int ShowWorldDisplay(GameObject target, Transform parent, string text, Vector3 offset)
    {
        GameObject newDisplay = Instantiate(worldDisplay, target.transform.position, Quaternion.identity, parent);
        newDisplay.GetComponent<Text>().text = text;
        newDisplay.GetComponent<WorldDisplayBehaviour>().SetTargetObject(target.transform.GetChild(0).gameObject);
        newDisplay.GetComponent<WorldDisplayBehaviour>().SetOffset(offset);
        int index = currentDisplays.Count;
        currentDisplays.Add(newDisplay);
        return index;
    }
}
