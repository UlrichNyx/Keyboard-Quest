using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    private DialogueTrigger trigger;
    private bool playerInRange;
    public bool active;
    void Start() 
    {
        playerInRange = false;
        active = false;
        trigger = GetComponent<DialogueTrigger>();
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange && !active)
        {
            trigger.TriggerDialogue();
            active = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && active)
        {
            trigger.NextDialogue();
            if(!trigger.IsActive())
            {
                active = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
