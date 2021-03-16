/* 
# Author: Filippos Kontogiannis
# Description: The umbrella class that gathers all scripts that have anything to do with the player
# Editors: ...
*/

// Standard Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerClass playerClass; // The placeholder variable for the chosen class of the player
    private int level; // The current level of the player (MAX 100)
    public Stats stats; // The current stats of the player
    //private GodFaith godFaith; // The chosen faith of the player

    //Initialize all variables on Start
    void Start()
    {
        playerClass = new PlayerClass();
        level = 1;
        stats = new Stats(playerClass, level);
    }
}

/* TODOS:
- Set a max limit for level

*/