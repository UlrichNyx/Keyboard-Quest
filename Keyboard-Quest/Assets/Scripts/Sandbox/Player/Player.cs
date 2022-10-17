/* 
# Author: Filippos Kontogiannis
# Description: The class that extends the entity class specifically for the player
# Editors: ...
*/

// Standard Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The total possible states the player can be in
public enum PlayerState
{
    idle,
    walk,
    interact,
    shopping
}

// This class inherits from Entity and extends it by adding the PlayerClass and the GodFaith
public class Player : Entity
{
    public PlayerState currentState; // The current state the player is in
    public PlayerClass playerClass; // The placeholder variable for the chosen class of the player
    public GodFaith godFaith; // The chosen faith of the player

    private static Player instance;


    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); 
    }
    
    //Initialize all variables on Start
    void Start()
    {
        stats.Initialize();
        Item[] items = this.GetComponent<Inventory>().items;
        foreach(Item i in items)
        {
            if(i != null && i.equipped)
            {
                i.equipped = false;
            }
        }
        currentState = PlayerState.idle; // Let the default state be idle
    }
}

/* TODOS:
- Set a max limit for level

*/