using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestionTrigger : MonoBehaviour
{
    public Question question;

    public void StartQuestion()
    {
        FindObjectOfType<QuestionManager>().StartQuestion(question);
    }

    public void MakeChoiceA()
    {
        question.functionA.Invoke();
    }
    public void MakeChoiceB()
    {
        question.functionB.Invoke();
    }
}
