/* 
# Author: Filippos Kontogiannis
# Description: The class for the editable dialogue objects
# Editors: ...
*/

// This class is taken from Brackeys: https://www.youtube.com/watch?v=_nRzoTzeyxU

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // This allows for objects of this class to be initialized through the editor
public class Dialogue // Doesn't inherit from MonoBehavior since its just a template class
{
    public string name; // The name of the NPC/Object that is talking
    [TextArea(3,10)] // The input for the dialogue in the editor
    public string[] sentences; // The actual dialogue
    public bool isQuestion; // Whether or not the dialogue ends with a choice the player must make
}

/* TODOS:

*/
