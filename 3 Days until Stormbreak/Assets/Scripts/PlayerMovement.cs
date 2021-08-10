/* 
# Author: Filippos Kontogiannis
# Description: The class that defines how the player moves
# Editors: ...
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class has been partially borrowed from: https://www.youtube.com/watch?v=--N5IgSUQWI&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=3 
public class PlayerMovement : MonoBehaviour
{
    public float speed; // The speed of the player
    private Player player; // Reference to the player object
    private Rigidbody2D playerRigidBody; // Used for collisions
    private Animator animator; // Used for changing sprite animations
    private Vector3 change; // Represents the change of position between frames
    private BoxCollider2D interactionBox; // the reference to the interaction box
    private bool sprinting; // Whether or not the player is sprinting, can be activated with R

    // Start is called before the first frame update
    void Start()
    {
        // Get the private components on the Player object
        player = GetComponent<Player>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        interactionBox = GetComponents<BoxCollider2D>()[1]; // Get the 2nd box collider (this is the trigger one)
        sprinting = false; // Default is for player not to be sprinting
    }


    // Check for Key Input:
    void Update()
    {
        
        // If R is pressed, change the speed and set sprinting to true
        if(Input.GetKeyDown(KeyCode.R) && !sprinting)
        {
            speed = 1.5f;
            sprinting = true;
        }
        else if(Input.GetKeyDown(KeyCode.R)) // Otherwise reset both speed and sprinting
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
        
        // If there was movement declared through wasd or the arrow keys and the player is not in a dialogue
        if(change != Vector3.zero && player.currentState != PlayerState.interact)
        {
            // Move the character and change the animations
            MoveCharacter();
            UpdateInteractionBox();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
            
        }
        else // if the player is not moving, set both state and animation to idle
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
        // If the player moves to the left, move the interaction box accordingly
        if(change.x < 0)
        {
            interactionBox.offset = new Vector2(-interactionBox.size.x + change.x * speed * Time.deltaTime, change.y * speed * Time.deltaTime);
        }
        else if(change.x > 0) // If the player moves to the right, move the interaction box accordingly
        {
            interactionBox.offset = new Vector2(interactionBox.size.x + change.x * speed * Time.deltaTime, change.y * speed * Time.deltaTime);
        }
        if(change.y > 0) // If the player moves upwards, move the interaction box accordingly
        {
            interactionBox.offset = new Vector2(change.x * speed * Time.deltaTime, change.y * speed * Time.deltaTime + interactionBox.size.y);
        }
        else if(change.y < 0) // If the player moves downwards, move the interaction box accordingly
        {
            interactionBox.offset = new Vector2(change.x * speed * Time.deltaTime, change.y * speed * Time.deltaTime - interactionBox.size.y);
        }
    }

    // Change the position vector of the palyer into this new one
    void MoveCharacter()
    {
        player.currentState = PlayerState.walk; // Change the state of the player
        change.Normalize(); // Normalize the vector so as to make the player have the same speed while moving diagonally
        playerRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime); // Add the change to the position vector of the player
    }
}

/*  TODOS:

*/
