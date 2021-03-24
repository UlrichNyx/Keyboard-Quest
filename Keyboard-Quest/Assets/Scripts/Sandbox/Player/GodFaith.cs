/* 
# Author: Filippos Kontogiannis
# Description: The class that defines the chosen godFaith of the player (aka what type of Magic Damage they do)
# Editors: ...
*/

// Standard Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Enables this class to appear on the editor
public class GodFaith
{
    public enum Faith
    {
        theForge,
        theSea,
        theMountain,
        theSky,
        theStorm,
        theSerpent,
        life,
        death,
        ultima
    }
    private string faithName; // The name of the chosen god
    public Faith faith;

    // Start is called on the first frame
    void Start()
    {
        if(faith == Faith.theForge)
        {
            faithName = "The Forge";
        }
        else if(faith == Faith.theSea)
        {
            faithName = "The Sea";
        }
        else if(faith == Faith.theMountain)
        {
            faithName = "The Mountain";
        }
        else if(faith == Faith.theSky)
        {
            faithName = "The Sky";
        }
        else if(faith == Faith.theStorm)
        {
            faithName = "The Storm";
        }
        else if(faith == Faith.theSerpent)
        {
            faithName = "The Serpent";
        }
        else if(faith == Faith.life)
        {
            faithName = "Life";
        }
        else if(faith == Faith.death)
        {
            faithName = "Death";
        }
        else if(faith == Faith.ultima)
        {
            faithName = "Ultima";
        }
        
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