using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameControlBehaviour : MonoBehaviour
{
    public static GameControlBehaviour instance = null;

    public List<QuestionScript> Questions;
    private TMP_Text Label;
    private List<TMP_Text> Buttons = new List<TMP_Text>();
    private GameObject Result;

    private int numberQuestions = 0;
    private int currentQuestion = 0;

    void Awake()
    {
        //Singleton Pattern (kind of)
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
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
    {
        Debug.Log(scene.name);
        if(scene.name == "QuizScene")
        {
            SetUpQuiz();
        }
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    //Quiz

    public void SetUpQuiz()
    {
        if (numberQuestions == 0) { numberQuestions = Questions.Count; }
        if (currentQuestion == 0) { currentQuestion = 1; }
        Fetch();
        HideResult();
        SetUpQuestion(currentQuestion);
    }

    public void Fetch()
    {
        Label = GameObject.FindGameObjectWithTag("Label").GetComponent<TMP_Text>();
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionA").GetComponentInChildren<TMP_Text>());
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionB").GetComponentInChildren<TMP_Text>());
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionC").GetComponentInChildren<TMP_Text>());
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionD").GetComponentInChildren<TMP_Text>());
        Result = GameObject.FindGameObjectWithTag("Result");
    }

    public void SetUpQuestion(int index)
    {
        Label.text = Questions[index].name;
        Buttons[0].text = Questions[index].optionA;
        Buttons[1].text = Questions[index].optionB;
        Buttons[2].text = Questions[index].optionC;
        Buttons[3].text = Questions[index].optionD;
        for(int i = 0; i < 4; i++)
        {
            Buttons[i].gameObject.GetComponent<Button>().onClick.AddListener( delegate{ ShowResult(index, i); } );;
        }
    }

    public void ShowResult(int index, int i)
    {
        Text T = Result.GetComponentInChildren<Text>();
        if (i == 0) T.text = Questions[index].answerA;
        else if (i == 1) T.text = Questions[index].answerB;
        else if (i == 2) T.text = Questions[index].answerC;
        else if (i == 3) T.text = Questions[index].answerD;
        else { Debug.Log("Erro"); };
        Result.SetActive(true);
    }

    public void HideResult()////////////////////////////////////////////////////////
    {
        Result.SetActive(false);
    }//////////////////////////////////////////////////////////////////////////////

    public void NextQuestion()
    {
        if(Questions[currentQuestion+1] != null)
        {
            ChangeScene("QuizScene");
        }
        else
        {
            WrapUpQuiz();
        }
    }

    public void WrapUpQuiz()
    {
        numberQuestions = 0;
        currentQuestion = 0;
        ChangeScene("MainMenu");
    }
}