/* 
# Author: Filippos Kontogiannis
# Description: The class for defining the chosen player class
# Editors: ...
*/

// Standard Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Allows this to show up on the editor as a choice
public class PlayerClass
{
    // The total number of classes
    public enum PClass
    {
        knight, // The melee class that holds a sword and a shield
        scout, // The stealthy class that wields daggers
        bard, // The witty class that wields musical instruments and plays songs to cast spells
        hunter, // The wild class that wields bows, lays traps and has a pet that follows them around
        mage, // The spellslinger class that type their spells to cast them
        gambler, // The luck based class that holds a deck of spells
        alchemist // The preparation based class that makes use of items such as potions etc.
    }
    public PClass playerClass = PClass.knight; // Let the default option be knight
    private string className; // The name of the chosen class

    // Constructor that defaults for the Knight class
    public PlayerClass()
    {
        if(playerClass == PClass.knight)
        {
            className = "Knight";
        }
        else if(playerClass == PClass.scout)
        {
            className = "Scout";
        }
        else if(playerClass == PClass.bard)
        {
            className = "Bard";
        }
        else if(playerClass == PClass.hunter)
        {
            className = "Hunter";
        }
        else if(playerClass == PClass.mage)
        {
            className = "Mage";
        }
        else if(playerClass == PClass.gambler)
        {
            className = "Gambler";
        }
        else if(playerClass == PClass.alchemist)
        {
            className = "Alchemist";
        }
        
    }
    
    // Get the private field className
    public string GetName()
    {
        return className;
    }
}

/* TODOS:

*/