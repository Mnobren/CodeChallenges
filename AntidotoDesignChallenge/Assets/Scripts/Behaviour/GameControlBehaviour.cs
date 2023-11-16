using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControlBehaviour : MonoBehaviour
{
    public static GameControlBehaviour instance = null;

    public QuestionScript[] Questions;
    private TMP_Text Label;
    private List<GameObject> Buttons = new List<GameObject>();
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
        if (numberQuestions == 0) { numberQuestions = Questions.Length; }
        if (currentQuestion == 0) { currentQuestion = 1; }
        Fetch();
        HideResult();
        SetUpQuestion(currentQuestion-1);
    }

    public void Fetch()
    {
        Buttons.Clear();
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionA"));
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionB"));
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionC"));
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionD"));
        Label = GameObject.FindGameObjectWithTag("Label").GetComponent<TMP_Text>();
        Result = GameObject.FindGameObjectWithTag("Result");
    }

    public void SetUpQuestion(int index)
    {
        Label.text = Questions[index].name;
        Buttons[0].GetComponentInChildren<TMP_Text>().text = Questions[index].optionA;
        Buttons[1].GetComponentInChildren<TMP_Text>().text = Questions[index].optionB;
        Buttons[2].GetComponentInChildren<TMP_Text>().text = Questions[index].optionC;
        Buttons[3].GetComponentInChildren<TMP_Text>().text = Questions[index].optionD;
        Buttons[0].gameObject.GetComponent<Button>().onClick.AddListener( delegate{ ShowResult(0, index); } );
        Buttons[1].gameObject.GetComponent<Button>().onClick.AddListener( delegate{ ShowResult(1, index); } );
        Buttons[2].gameObject.GetComponent<Button>().onClick.AddListener( delegate{ ShowResult(2, index); } );
        Buttons[3].gameObject.GetComponent<Button>().onClick.AddListener( delegate{ ShowResult(3, index); } );
    }

    public void ShowResult(int i, int index)
    {
        Result.SetActive(true);
        TMP_Text T = Result.GetComponentInChildren<TMP_Text>();
        if (i == 0) T.text = Questions[index].answerA;
        else if (i == 1) T.text = Questions[index].answerB;
        else if (i == 2) T.text = Questions[index].answerC;
        else if (i == 3) T.text = Questions[index].answerD;
        else { Debug.Log("Erro"); };
        Result.GetComponentInChildren<Button>().onClick.AddListener( NextQuestion );
    }

    public void HideResult()
    {
        Result.SetActive(false);
    }

    public void NextQuestion()
    {
        currentQuestion++;
        if(currentQuestion <= numberQuestions)
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