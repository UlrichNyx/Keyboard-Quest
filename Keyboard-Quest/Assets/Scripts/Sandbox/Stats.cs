using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public float resilience;
    public float strength;
    public float dexterity;
    public float wit;
    public float luck;
    public float faith;

    public Stats(float resilience, float strength, float dexterity, float wit, float luck, float faith)
    {
        this.resilience = resilience;
        this.strength = strength;
        this.dexterity = dexterity;
        this.wit = wit;
        this.luck = luck;
        this.faith = faith;
    }

    public Stats(Player_Class player_class, int level)
    {
        if(player_class.GetName() == "Knight")
        {
            this.resilience = level * 2 + 10;
            this.strength = level * 2 + 10;
            this.dexterity = level + 5;
            this.wit = level + 5;
            this.luck = level + 5;
            this.faith = level + 5;
        }
    }

}
