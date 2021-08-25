/* 
# Author: Filippos Kontogiannis
# Description: The base class for a question that comes at the end of a Dialogue object
# Editors: ...
*/

/* TODOS:

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Necessary for local functions

[System.Serializable]
public class Question
{
    public string optionA; // What should be displayed on the question box
    public UnityEvent functionA; // What selecting optionA will do

    public string optionB; // What should be displayed on the question box
    public UnityEvent functionB; // What selecting optionB will do
}
