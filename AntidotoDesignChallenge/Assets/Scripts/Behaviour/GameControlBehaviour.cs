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
    //Controla o jogo como um todo, por isso persiste entre cenas

    public static GameControlBehaviour instance = null;

    public QuestionScript[] Questions;
    private TMP_Text Label;
    private List<GameObject> Buttons = new List<GameObject>();
    private GameObject Result;
    private TMP_Text Counter;

    private int numberQuestions = 0;
    private int currentQuestion = 0;
    private int hit = 0;

    void Awake()//Persistência entre cenas
    {
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

    void OnEnable()
    {//Adiciona um delegate apontando para LevelJustLoaded
        SceneManager.sceneLoaded += LevelJustLoaded;
    }

    void OnDisable()
    {//Subtrai um delegate apontando para LevelJustLoaded
        SceneManager.sceneLoaded -= LevelJustLoaded;
    }

    void LevelJustLoaded(Scene scene, LoadSceneMode mode)//É executado sempre que uma cena é carregada
    {
        if(scene.name == "QuizScene")//No caso de a cena ser QuizScene
        {
            SetUpQuiz();//Prepara o Ouiz
        }
    }

    public void ChangeScene(string name)//Troca para outra cena
    {
        SceneManager.LoadScene(name);
    }

    //Quiz

    public void SetUpQuiz()
    {
        if (numberQuestions == 0) { numberQuestions = Questions.Length; }//Caso o quiz tenha acabedo de começar configura as variáveis de controle
        if (currentQuestion == 0) { currentQuestion = 1; }//Caso o quiz tenha acabedo de começar configura as variáveis de controle
        Fetch();//Busca pelos GameObjects relevantes para controlar o jogo
        HideResult();//Esconde a janela que exibe o resultado
        SetUpQuestion(currentQuestion-1);//Prepara a questão atual
        Counter.text = currentQuestion+"/"+numberQuestions;//Atualiza o índice de questões
    }

    public void Fetch()//Busca pelos GameObjects relevantes para controlar o jogo
    {
        Buttons.Clear();//Esvazia a lista para previvinir redundâncias
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionA"));
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionB"));
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionC"));
        Buttons.Add(GameObject.FindGameObjectWithTag("OptionD"));
        Counter = GameObject.FindGameObjectWithTag("Counter").GetComponent<TMP_Text>();
        Label = GameObject.FindGameObjectWithTag("Label").GetComponent<TMP_Text>();
        Result = GameObject.FindGameObjectWithTag("Result");
    }

    public void SetUpQuestion(int index)//Prepara a questão atual
    {
        Label.text = Questions[index].name;//Exibe a questão
        Buttons[0].GetComponentInChildren<TMP_Text>().text = Questions[index].optionA;//Exibe a resposta
        Buttons[1].GetComponentInChildren<TMP_Text>().text = Questions[index].optionB;//Exibe a resposta
        Buttons[2].GetComponentInChildren<TMP_Text>().text = Questions[index].optionC;//Exibe a resposta
        Buttons[3].GetComponentInChildren<TMP_Text>().text = Questions[index].optionD;//Exibe a resposta
        Buttons[0].gameObject.GetComponent<Button>().onClick.AddListener( delegate{ ShowResult(0, index); } );//Prepara o resultado correspondente
        Buttons[1].gameObject.GetComponent<Button>().onClick.AddListener( delegate{ ShowResult(1, index); } );//Prepara o resultado correspondente
        Buttons[2].gameObject.GetComponent<Button>().onClick.AddListener( delegate{ ShowResult(2, index); } );//Prepara o resultado correspondente
        Buttons[3].gameObject.GetComponent<Button>().onClick.AddListener( delegate{ ShowResult(3, index); } );//Prepara o resultado correspondente
    }

    public void ShowResult(int i, int index)//Abre e configura a janela que exibe o resultado
    {
        Result.SetActive(true);//Abre a janela
        TMP_Text T = Result.GetComponentInChildren<TMP_Text>();//Busca pelo display de resultado

        //Exibe o resultado de acordo com a resposta
        if (i == 0) { T.text = Questions[index].answerA; }
        else if (i == 1) { T.text = Questions[index].answerB; }
        else if (i == 2) { T.text = Questions[index].answerC; }
        else if (i == 3) { T.text = Questions[index].answerD; }
        else { Debug.Log("Erro"); };

        if (i == Questions[index].optionsRight) { hit++; }//Anota a pontuação caso a resposta esteja correta
        if (NextQuestion())//Descobre se tem mais alguma pergunta
        {
            Result.GetComponentInChildren<Button>().onClick.AddListener( delegate { ChangeScene("QuizScene"); } );//Prepara o botão para continuar
            Result.GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text = "PRÓXIMO";//Escreve no botão
        }
        else 
        {
            Result.GetComponentInChildren<Button>().onClick.AddListener( WrapUpQuiz );//Prepara o botão para finalizar
            Result.GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text = hit+" ACERTOS";//Escreve no botão
        }
    }

    public void HideResult()//Esconde a janela que exibe o resultado
    {
        Result.SetActive(false);
    }

    public bool NextQuestion()//Descobre se tem mais alguma pergunta
    {
        currentQuestion++;
        if(currentQuestion <= numberQuestions)//Caso o número da proxima questão seja menor ou igual à quantidade de questões
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void WrapUpQuiz()//Finaliza o Quiz e volta para o começo
    {
        numberQuestions = 0;//Reinicia as varáveis de controle
        currentQuestion = 0;//Reinicia as varáveis de controle
        hit = 0;//Reinicia a pontuação
        ChangeScene("MainMenu");//Volta para o começo
    }
}