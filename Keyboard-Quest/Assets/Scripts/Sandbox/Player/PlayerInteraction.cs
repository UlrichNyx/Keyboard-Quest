/* 
# Author: Filippos Kontogiannis
# Description: The class for defining the interaction of the player with interactable objects or NPCs
# Editors: ...
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Player player; // The reference to the player
    private BoxCollider2D hitbox; // The reference to the invisible hitbox that is in front of the player
    private DialogueTrigger trigger; // The reference to the trigger that is to be retrieved
    private QuestionTrigger qTrigger;
    private GameObject npc; // The reference to the NPC that was interacted

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        hitbox = GetComponents<BoxCollider2D>()[1]; // GetComponents is used here because there are multiple BoxCollider2D for the player (the second one is the one we want)
        hitbox.enabled = false; // Set the hitbox to false so as to not trigger any conversations yet
        npc = null; // Set the reference to the NPC to null since we havent interacted with anyone yet
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // If the space key is pressed by the player
        {
            if(player.currentState == PlayerState.idle || player.currentState == PlayerState.walk) // If the player is either idle or walking
            {
                StartCoroutine(EnableHitbox()); // Enable the hitbox, possibly triggering a dialogue
            }   
            if(player.currentState == PlayerState.interact) // If the player is already in a conversation
            {
                trigger.NextDialogue(); // Get the next sentence
                if(!trigger.IsActive()) // Check if the dialogue is still active
                {
                    player.currentState = PlayerState.idle; // If not, then get the player out of the interact state
                    if(npc != null) // If the player interacted with an NPC
                    {
                        npc.GetComponent<NPCBehavior>().currentState = NPCBehavior.NPCState.idle; // Reset the NPC's state too
                        npc = null; // Set the reference to null
                    }
                    
                    hitbox.enabled = false; // Disable the hitbox
                }
            }
        }
    }

    // A coroutine is used to enable the hitbox and shortly after disable it
    private IEnumerator EnableHitbox()
    {
        hitbox.enabled = true;
        yield return new WaitForSeconds(0.1f);
        hitbox.enabled = false;
    }

    // If the hitbox comes into contact with anything
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(hitbox.enabled) // If the hitbox is enabled
        {
            if(other.CompareTag("Interactable")) // If the tag of the object was "Interactable"
            {
                player.currentState = PlayerState.interact; // Set the player into the interact state
                trigger = other.GetComponent<DialogueTrigger>();
                if(trigger.dialogue.isQuestion)
                {
                    qTrigger = other.GetComponent<QuestionTrigger>();
                    if(trigger.dialogue.isQuestion)
                    {
                        qTrigger = other.GetComponent<QuestionTrigger>();
                        qTrigger.StartQuestion();
                    } 
                } 
                trigger.TriggerDialogue(); // Trigger the dialogue
            }
            else if(other.CompareTag("NPC")) // If the tag of the object was "NPC"
            {
                player.currentState = PlayerState.interact; 
                other.GetComponent<NPCBehavior>().currentState = NPCBehavior.NPCState.interact; // Set the NPC's state to interact too
                npc = other.gameObject; // Set the reference to the NPC
                npc.GetComponent<NPCBehavior>().TurnTowards(new Vector2(transform.position.x, transform.position.y)); // Make the NPC turn towards the player
                trigger = other.GetComponent<DialogueTrigger>();
                if(trigger.dialogue.isQuestion)
                {
                    qTrigger = other.GetComponent<QuestionTrigger>();
                    qTrigger.StartQuestion();
                } 
                trigger.TriggerDialogue(); // Trigger the dialogue
            }
            else if(other.CompareTag("Pickup"))
            {
                Debug.Log("Interacted with Pickup");
                if(this.GetComponent<Inventory>().AddItem(other.GetComponent<PickUp>().item))
                {
                    Destroy(other.GetComponent<PickUp>().gameObject);
                }
                else
                {
                    Debug.Log("Inventory is full!");
                }
            }
            if(other.CompareTag("Door"))
            {
                Debug.Log("Interacted with Door");
                FindObjectOfType<Door>().GetComponent<Door>().GoToDestination();
            }
        }
        if(other.CompareTag("AreaBox"))
        {
            Debug.Log("Interacted with AreaBox");
            FindObjectOfType<TitleManager>().GetComponent<TitleManager>().inNewArea = true;
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("AreaBox"))
        {
            Debug.Log("Left AreaBox");
            FindObjectOfType<TitleManager>().GetComponent<TitleManager>().inNewArea = false;
        }
    }

}

/* TODOS:

*/