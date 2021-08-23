using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public Question question;
    public Animator animator;

    public void StartQuestion(Question question)
    {
        this.question = question;
    }
    public void MakeChoiceA()
    {
        Debug.Log(question.optionA);
        question.functionA.Invoke();
    }
    public void MakeChoiceB()
    {
        Debug.Log("You chose no");
        question.functionB.Invoke();
    }

    public void ShowQuestionBox()
    {
        animator.SetBool("IsOpen", true);
    }

    public void HideQuestionBox()
    {
        animator.SetBool("IsOpen", false);
    }
}
