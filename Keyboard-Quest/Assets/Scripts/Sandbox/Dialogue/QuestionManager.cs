using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Question question;
    public Text textA;
    public Text textB;
    public Animator animator;

    public void StartQuestion(Question question)
    {
        this.question = question;
        this.textA.text = question.optionA;
        this.textB.text = question.optionB;
    }
    public void MakeChoiceA()
    {
        Debug.Log(question.optionA);
        question.functionA.Invoke();
        animator.SetBool("IsOpen", false);
    }
    public void MakeChoiceB()
    {
        Debug.Log("You chose no");
        question.functionB.Invoke();
        animator.SetBool("IsOpen", false);
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
