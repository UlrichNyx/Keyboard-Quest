/* 
# Author: Filippos Kontogiannis
# Description: The script for the component to be used on NPCs with questions
# Editors: ...
*/

/* TODOS:

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestionTrigger : MonoBehaviour
{
    public Question question; // The question the NPC asks

    public Dialogue reply;

    public void StartQuestion() // To be called when the NPC is talked to
    {
        FindObjectOfType<QuestionManager>().StartQuestion(question);
    }

    public void StartReply()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(reply);
    }
}
