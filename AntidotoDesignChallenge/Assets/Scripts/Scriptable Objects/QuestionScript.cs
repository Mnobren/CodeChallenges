using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Scriptable Object/Question")]
public class QuestionScript : ScriptableObject
{
    [TextArea]public new string name;
    public string optionA;
    public string optionB;
    public string optionC;
    public string optionD;
    public string answerA;
    public string answerB;
    public string answerC;
    public string answerD;
}