/* 
# Author: Filippos Kontogiannis
# Description: The class for defining the chosen player class
# Editors: ...
*/

// Standard Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass
{
    private string className; // The name of the chosen class
    private string[] classes = {"Knight"}; // The range of possible classes (to avoid errors)

    // Constructor that defaults for the Knight class
    public PlayerClass()
    {
        className = classes[0];
    }

    // Get the private field className
    public string GetName()
    {
        return className;
    }
}
