/* 
# Author: Filippos Kontogiannis
# Description: The class that defines the chosen godFaith of the player (aka what type of Magic Damage they do)
# Editors: ...
*/

// Standard Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodFaith
{
    private string faithName; // The name of the chosen god
    private string[] faiths = {"Fire"}; // The wide range of gods that can be chosen (to avoid errors)

    // Constructor used for defaulting to the "Fire" faith
    public GodFaith()
    {
        faithName = faiths[0];
    }

    // Get the private field: faithName
    public string GetName()
    {
        return faithName;
    }
}

/* TODOS:
 - ...
*/