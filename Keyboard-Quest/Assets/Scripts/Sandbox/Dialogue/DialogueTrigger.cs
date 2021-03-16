using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void NextDialogue()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

    public bool IsActive()
    {
        return FindObjectOfType<DialogueManager>().IsActive();
    }
}
