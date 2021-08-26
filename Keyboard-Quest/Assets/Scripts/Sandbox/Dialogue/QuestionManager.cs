/* 
# Author: Filippos Kontogiannis
# Description: The class which handles all question interactions
# Editors: ...
*/

/* TODOS:

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessary for working with UI

public class QuestionManager : MonoBehaviour
{
    [HideInInspector]
    public Question question; // The current question being asked
    public Text textA; // The first option's text
    public Text textB; // The second option's text
    public Animator animator; // The animator responsible for swooping in the box at the right time

    public void StartQuestion(Question question) // To be called when talking to an NPC with a question
    {
        this.question = question;
        this.textA.text = question.optionA;
        this.textB.text = question.optionB;
    }
    public void MakeChoiceA() // If the first choice is chosen
    {
        question.functionA.Invoke(); // Invoke the local function which is binded to the first option
        HideQuestionBox();
    }
    public void MakeChoiceB() // If the second choice is chosen
    {
        question.functionB.Invoke(); // Invoke the local function which is binded to the second option
        HideQuestionBox();
    }

    public void ShowQuestionBox() // Use when question appears
    {
        animator.SetBool("IsOpen", true);
    }

    public void HideQuestionBox() // Use when question is done
    {
        animator.SetBool("IsOpen", false);
    }
}
