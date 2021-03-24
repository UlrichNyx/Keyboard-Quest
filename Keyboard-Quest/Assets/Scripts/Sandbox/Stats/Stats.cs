/* 
# Author: Filippos Kontogiannis
# Description: The class that is used by all players/enemies/allies in order to define their stats in combat
# Editors: ...
*/

// Standard Imports
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    // There are 6 stats in total. 
    // They have been left as public so as to spare the set/get functions
    [Range(0, 100)]
    public int resilience; // HP
    [Range(0, 100)]
    public int strength; // Affects damage caused by heavy weapons
    [Range(0, 100)]
    public int dexterity; // Affects damage caused by light weapons
    [Range(0, 100)]
    public int wit; // Armor and Magic Resistance
    [Range(0, 100)]
    public int luck; // Critical Strike chance
    [Range(0, 100)]
    public int faith; // Magic Damage

    // Constructor used for setting Stats explicitly
    public Stats(int resilience, int strength, int dexterity, int wit, int luck, int faith)
    {
        this.resilience = resilience;
        this.strength = strength;
        this.dexterity = dexterity;
        this.wit = wit;
        this.luck = luck;
        this.faith = faith;
    }

    // Constructor used for setting Stats according to player class, level and god faith (Not yet fully implemented)
    public Stats(PlayerClass playerClass, int level)
    {
        // The starting stat is 5
        // Given a certain class, certain proficiencies start from 10 and have a *2 multiplier per level

        // Knight: Proficiency in resilience and strength
        if(playerClass.GetName() == "Knight") 
        {
            this.resilience = level * 2 + 10;
            this.strength = level * 2 + 10;
            this.dexterity = level + 5;
            this.wit = level + 5;
            this.luck = level + 5;
            this.faith = level + 5;
        }
        // Scout: Proficiency in resilience and strength
        // Bard: Proficiency in resilience and dexterity
        // Hunter: Proficiency in strength and dexterity
        // Mage: Proficiency in wit and faith
        // Gambler: Proficiency in luck and faith
        // Alchemist: Proficiency in dexterity and wit
    }

    /* TODOS:
    - Complete proficiencies for all classes + god faiths
    - Add functions for simply incrementing a stat
    - Set limit to 100 for each stat
    - Create functions/databases for enemy/ally stats
    */

}
