using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Instruction
{
    public enum Command
    {
        idleLeft,
        idleRight,
        idleDown,
        idleUp,
        walkLeft,
        walkRight,
        walkDown,
        walkdUp
    }
    public Command command = Command.idleLeft;
    [Range(0f,10f)]
    public float duration; 
}
