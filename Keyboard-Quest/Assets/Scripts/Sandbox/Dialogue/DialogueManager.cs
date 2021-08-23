/* 
# Author: Filippos Kontogiannis
# Description: The class responsible for managing a dialogue by starting it, iterating it and ending it
# Editors: ...
*/

// This class is taken from Brackeys: https://www.youtube.com/watch?v=_nRzoTzeyxU

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessary import for working with UI

public class DialogueManager : MonoBehaviour // This script should be attached to a DialogueManager object
{
    private Queue<string> sentences; // Current dialogue sentences
    private bool isQuestion;
    private QuestionTrigger qTrigger;
    public Text dialogueText; // The text that displays on the UI
    public Text nameText; // The name of the NPC that displays on the UI
    public Animator animator; // The animator of the dialogue box --> for bringing it into and out from the screen


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); // A queue is used due to its FIFO
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true); // Bring up the dialogue box
        sentences.Clear(); // Clear anything that might have been in the sentences
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); // Enqueue every sentence from the new dialogue object
        }
        nameText.text = dialogue.name; // Change the name of who is speaking
        isQuestion = dialogue.isQuestion;
        DisplayNextSentence(); // Display the first sentence
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 1 && isQuestion) // If there are no more sentences
        { 
            FindObjectOfType<QuestionManager>().ShowQuestionBox();
        }
        else if(sentences.Count == 0) // If there are no more sentences
        { 
            EndDialogue(); // Stop the dialogue
            FindObjectOfType<QuestionManager>().HideQuestionBox();
            return;
        }
        
        // Otherwise
        string sentence = sentences.Dequeue(); // Bring out the next sentence
        StopAllCoroutines(); // Stop typing out the previous one
        StartCoroutine(TypeSentence(sentence)); // Display the next one
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = ""; // Start from an empty string
        foreach(char letter in sentence.ToCharArray()) // Go through the sentence char by char
        {
            FindObjectOfType<AudioManager>().Play("Murmur"); // Play a murmur sound for each char (according to sound length)
            dialogueText.text += letter; // Add the next letter
            yield return null; // Call this function on every frame
        }
    }

    // Check if the dialogue has ended (used for changing states of other objects etc.)
    public bool IsActive()
    {
        return animator.GetBool("IsOpen");
    }

    // End the dialogue, thus closing the dialogue box
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}

/* TODOS:

*/
