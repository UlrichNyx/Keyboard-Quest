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
    public int RES; // MAX HP
    [Range(0, 100)]
    public int STR; // Affects damage caused by heavy weapons
    [Range(0, 100)]
    public int DEX; // Affects damage caused by light weapons
    [Range(0, 100)]
    public int WIT; // Armor and Magic Resistance
    [Range(0, 100)]
    public int LCK; // Critical Strike chance
    [Range(0, 100)]
    public int FTH; // Magic Damage

    private Stats tempStatChange;

    public static int MAX_STAT = 100;
    public static int MIN_STAT = 0;

    // Constructor used for setting Stats explicitly
    public Stats(int resilience, int strength, int dexterity, int wit, int luck, int faith)
    {
        this.RES = resilience;
        this.STR = strength;
        this.DEX = dexterity;
        this.WIT = wit;
        this.LCK = luck;
        this.FTH = faith;
    }

    // Constructor used for setting Stats according to player class, level and god faith (Not yet fully implemented)
    public Stats(PlayerClass playerClass, int level)
    {
        // The starting stat is 5
        // Given a certain class, certain proficiencies start from 10 and have a *2 multiplier per level

        // Knight: Proficiency in resilience and strength
        if(playerClass.GetName() == "Knight") 
        {
            this.RES = level * 2 + 10;
            this.STR = level * 2 + 10;
            this.DEX = level + 5;
            this.WIT = level + 5;
            this.LCK = level + 5;
            this.FTH = level + 5;
        }
        // Scout: Proficiency in resilience and strength
        // Bard: Proficiency in resilience and dexterity
        // Hunter: Proficiency in strength and dexterity
        // Mage: Proficiency in wit and faith
        // Gambler: Proficiency in luck and faith
        // Alchemist: Proficiency in dexterity and wit
    }

    public void AddStats(Stats stats)
    {
        this.RES += stats.RES;
        this.STR += stats.STR;
        this.DEX += stats.DEX;
        this.WIT += stats.WIT;
        this.LCK += stats.LCK;
        this.FTH += stats.FTH;

        MaintainStats();
    }
    public void RemoveStats(Stats stats)
    {
        this.RES -= stats.RES;
        this.STR -= stats.STR;
        this.DEX -= stats.DEX;
        this.WIT -= stats.WIT;
        this.LCK -= stats.LCK;
        this.FTH -= stats.FTH;

        MaintainStats();
    }

    private void MaintainStats()
    {
        this.RES = Mathf.Min(this.RES, MAX_STAT);
        this.STR = Mathf.Min(this.STR, MAX_STAT);
        this.DEX = Mathf.Min(this.DEX, MAX_STAT);
        this.WIT = Mathf.Min(this.WIT, MAX_STAT);
        this.LCK = Mathf.Min(this.LCK, MAX_STAT);
        this.FTH = Mathf.Min(this.FTH, MAX_STAT);

        this.RES = Mathf.Max(this.RES, MIN_STAT);
        this.STR = Mathf.Max(this.STR, MIN_STAT);
        this.DEX = Mathf.Max(this.DEX, MIN_STAT);
        this.WIT = Mathf.Max(this.WIT, MIN_STAT);
        this.LCK = Mathf.Max(this.LCK, MIN_STAT);
        this.FTH = Mathf.Max(this.FTH, MIN_STAT);
    }

    /* TODOS:
    - Complete proficiencies for all classes + god faiths
    - Add functions for simply incrementing a stat
    - Set limit to 100 for each stat
    - Create functions/databases for enemy/ally stats
    */

}
