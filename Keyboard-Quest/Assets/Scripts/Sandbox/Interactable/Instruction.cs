/* 
# Author: Filippos Kontogiannis
# Description: The class that defines the Instructions that NPCs have regarding their behavior!
# Editors: ...
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To see how these commands are distinctly treated, head over to the NPCBehavior script
[System.Serializable]
public class Instruction
{
    // An instruction object will have one of the following commands:
    public enum Command
    {
        idleLeft,
        idleRight,
        idleDown,
        idleUp,
        walkLeft,
        walkRight,
        walkDown,
        walkUp
    }
    public Command command = Command.idleLeft;
    [Range(0f,10f)]
    // The duration for which to follow this command
    public float duration; 
}

/* TODOS:

*/