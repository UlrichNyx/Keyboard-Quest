using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Player player;
    private BoxCollider2D hitbox;
    private DialogueTrigger trigger;
    private GameObject npc;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        hitbox = GetComponents<BoxCollider2D>()[1];
        hitbox.enabled = false;
        npc = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(player.currentState == PlayerState.idle || player.currentState == PlayerState.walk)
            {
                StartCoroutine(EnableHitbox());
            }   
            if(player.currentState == PlayerState.interact)
            {
                trigger.NextDialogue();
                if(!trigger.IsActive())
                {
                    player.currentState = PlayerState.idle;
                    if(npc != null)
                    {
                        npc.GetComponent<NPCBehavior>().currentState = NPCBehavior.NPCState.idle;
                        npc = null;
                    }
                    
                    hitbox.enabled = false;
                }
            }
        }
    }

    private IEnumerator EnableHitbox()
    {
        hitbox.enabled = true;
        yield return new WaitForSeconds(0.1f);
        hitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(hitbox.enabled)
        {
            if(other.CompareTag("Interactable"))
            {
                player.currentState = PlayerState.interact;
                trigger = other.GetComponent<DialogueTrigger>();
                trigger.TriggerDialogue();
            }
            else if(other.CompareTag("NPC"))
            {
                player.currentState = PlayerState.interact;
                other.GetComponent<NPCBehavior>().currentState = NPCBehavior.NPCState.interact;
                npc = other.gameObject;
                trigger = other.GetComponent<DialogueTrigger>();
                trigger.TriggerDialogue();
            }
        }
        
    }

}
