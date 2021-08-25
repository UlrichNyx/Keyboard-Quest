/* 
# Author: Filippos Kontogiannis
# Description: This script should be attached to all objects that trigger a dialogue
# Editors: ...
*/

/* TODOS:

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class essentially acts as an intermediary between the DialogueManager and whatever GameObject is using it
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; // The Dialogue object that is to be edited in the inspector

    // Trigger the dialogue, thus bringing up the box and displaying the first sentence
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    // Display the next sentence
    public void NextDialogue()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

    // Check whether or not the dialogue box is active
    public bool IsActive()
    {
        return FindObjectOfType<DialogueManager>().IsActive();
    }
}

