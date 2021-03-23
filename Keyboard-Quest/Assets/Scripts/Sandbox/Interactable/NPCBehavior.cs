using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public Instruction[] instructions;
    public bool loops;
    private Animator animator;
    private int counter;
    private float timer;

    public NPCState currentState;

    public enum NPCState
    {
        idle,
        interact
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        counter = 0;
        timer = 0f;
        currentState = NPCState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != NPCState.interact)
        {
            timer += Time.deltaTime;
        
            if(timer <= instructions[counter].duration)
            {
                ExecuteInstruction(instructions[counter]);
            }
            else
            {
                counter += 1;
                if(counter == instructions.Length)
                {
                    if(loops)
                    {
                        counter = 0;
                    }
                }
                timer = 0;
            }

        }

    }

    private void ExecuteInstruction(Instruction instruction)
    {
        if(instruction.command == Instruction.Command.idleLeft)
        {
            animator.SetFloat("MoveX", -1);
            animator.SetFloat("MoveY", 0);
        } 

        else if(instruction.command == Instruction.Command.idleRight)
        {
            animator.SetFloat("MoveX", 1);
            animator.SetFloat("MoveY", 0);
        }
        else if(instruction.command == Instruction.Command.idleDown)
        {
            animator.SetFloat("MoveY", 1);
            animator.SetFloat("MoveX", 0);
        }
        else if(instruction.command == Instruction.Command.idleUp)
        {
            animator.SetFloat("MoveY", -1);
            animator.SetFloat("MoveX", 0);
        }
    }
}
