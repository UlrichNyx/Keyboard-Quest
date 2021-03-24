/* 
# Author: Filippos Kontogiannis
# Description: The class that defines the chosen godFaith of the player (aka what type of Magic Damage they do)
# Editors: ...
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Player player;
    private Rigidbody2D playerRigidBody; // Used for collisions
    private Animator animator; // Used for changing sprite animations
    private Vector3 change; // Represents the change of position between frames
    private BoxCollider2D interactionBox;
    private bool sprinting;
    // Start is called before the first frame update
    void Start()
    {
        // Get the private components on the Player object
        player = GetComponent<Player>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        interactionBox = GetComponents<BoxCollider2D>()[1];
        sprinting = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && !sprinting)
        {
            speed = 1.5f;
            sprinting = true;
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            speed = 1f;
            sprinting = false;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update the change vector
        change = Vector3.zero;
        change.x  = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        
        // If there was movement declared through wasd or the arrow keys
        if(change != Vector3.zero && player.currentState != PlayerState.interact)
        {
            // Move the character and change the animations
            MoveCharacter();
            UpdateInteractionBox();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
            
        }
        else
        {
            animator.SetBool("Moving", false);
            if(player.currentState != PlayerState.interact)
            {
                player.currentState = PlayerState.idle;
            }
        }
    }

    void UpdateInteractionBox()
    {
        if(change.x < 0)
        {
            interactionBox.offset = new Vector2(-interactionBox.size.x + change.x * speed * Time.deltaTime, change.y * speed * Time.deltaTime);
        }
        else if(change.x > 0)
        {
            interactionBox.offset = new Vector2(interactionBox.size.x + change.x * speed * Time.deltaTime, change.y * speed * Time.deltaTime);
        }
        if(change.y > 0)
        {
            interactionBox.offset = new Vector2(change.x * speed * Time.deltaTime, change.y * speed * Time.deltaTime + interactionBox.size.y);
        }
        else if(change.y < 0)
        {
            interactionBox.offset = new Vector2(change.x * speed * Time.deltaTime, change.y * speed * Time.deltaTime - interactionBox.size.y);
        }
    }

    // Change the position vector of the palyer into this new one
    void MoveCharacter()
    {
        player.currentState = PlayerState.walk;
        change.Normalize();
        playerRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}

/*  TODOS:
*/