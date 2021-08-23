/* 
# Author: Filippos Kontogiannis
# Description: The class for defining how the NPC will react to Instructions
# Editors: ...
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public Instruction[] instructions; // The list of Instructions the NPC has
    public bool loops; // Whether or not the NPC should loop through these instructions
    private Animator animator; // The reference to the animator object that helps change animations
    private int counter; // The index of the current instruction
    private float timer; // The timer that keeps track of the duration of each instruction
    private float speed; // The speed at which the NPC moves
    private SpriteRenderer sprite; // The sprite of the NPC

    public NPCState currentState; // The current state of the NPC

    // The number of possible states
    public enum NPCState
    {
        idle,
        walk,
        interact
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        counter = 0;
        timer = 0f;
        speed = 0.5f;
        currentState = NPCState.idle; // Set the default state to idle
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != NPCState.interact) // If the NPC is not interacting with someone
        {
            timer += Time.deltaTime; // Increment the timer
        
            if(timer <= instructions[counter].duration) // If the current instruction is not done
            {
                ExecuteInstruction(instructions[counter]); // Execute the current instruction again
            }
            else
            {
                counter += 1; // Move on to the next instruction
                if(counter == instructions.Length)
                {
                    if(loops) // If the instructions loop go back to the beginning
                    {
                        counter = 0;
                    }
                }
                timer = 0; // Reset the timer
            }

        }

    }

    // Turn towards the player when they interact
    public void TurnTowards(Vector2 PlayerPos) // PlayerPos is a Vector2 with the x and y coordinates of the player
    {
        Instruction inst = new Instruction(); // Make a temporary instruction
        
        if(PlayerPos.x < transform.position.x)
        {
            inst.command = Instruction.Command.idleLeft; // If the player is on the left, turn left
        }
        else if(PlayerPos.x > transform.position.x)
        {
            inst.command = Instruction.Command.idleRight; // If the player is on the right, turn right
        }
        if(PlayerPos.y > transform.position.y + sprite.size.y) // Use the size of the NPC's sprite to determine whether or not to look up or left/right
        {
            inst.command = Instruction.Command.idleUp; // If the player is below, turn downwards
        }
        else if(PlayerPos.y < transform.position.y - sprite.size.y) 
        {
            inst.command = Instruction.Command.idleDown; // If the player is above, turn upwards
        }

        ExecuteInstruction(inst); // Execute the instruction (look towards a certain way)
    }

    // Execute the given instruction
    private void ExecuteInstruction(Instruction instruction)
    {
        if(instruction.command == Instruction.Command.idleLeft) // Look left
        {
            animator.SetFloat("MoveX", -1); // Giving a positive MoveX turns to the right/ negative to the left, 0 does nothing
            animator.SetFloat("MoveY", 0); // Giving a positive MoveY turns upwards/ negative downwards, 0 does nothing
            animator.SetBool("Moving", false); // Giving a true Moving triggers the walking animation, false turns it off
        } 

        else if(instruction.command == Instruction.Command.idleRight) // Look right
        {
            animator.SetFloat("MoveX", 1);
            animator.SetFloat("MoveY", 0);
            animator.SetBool("Moving", false);
        }
        else if(instruction.command == Instruction.Command.idleDown) // Look down
        {
            animator.SetFloat("MoveY", -1);
            animator.SetFloat("MoveX", 0);
            animator.SetBool("Moving", false);
        }
        else if(instruction.command == Instruction.Command.idleUp) // Look up
        {
            animator.SetFloat("MoveY", 1);
            animator.SetFloat("MoveX", 0);
            animator.SetBool("Moving", false);
        }
        else if(instruction.command == Instruction.Command.walkUp) // Walk up
        {
            animator.SetFloat("MoveY", 1);
            animator.SetFloat("MoveX", 0);
            animator.SetBool("Moving", true);
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z); // Change the NPC's position
        }
        else if(instruction.command == Instruction.Command.walkDown) // Walk down
        {
            animator.SetFloat("MoveY", -1);
            animator.SetFloat("MoveX", 0);
            animator.SetBool("Moving", true);
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        }
        else if(instruction.command == Instruction.Command.walkRight) // Walk right
        {
            animator.SetFloat("MoveY", 0);
            animator.SetFloat("MoveX", 1);
            animator.SetBool("Moving", true);
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if(instruction.command == Instruction.Command.walkLeft) // Walk left
        {
            animator.SetFloat("MoveY", 0);
            animator.SetFloat("MoveX", -1);
            animator.SetBool("Moving", true);
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
}

/* TODOS:

*/