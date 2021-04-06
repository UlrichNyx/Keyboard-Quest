/* 
# Author: Filippos Kontogiannis
# Description: The class for defining Items
# Editors: ...
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Item types:

- Change your stats through use
- Change your stats through equipping
- Give Experience points
- Act as weapons/shields/spellbooks/bows/decks
- Give special abilities/buffs

*/

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] // Allows this to be created through right clicking on the project folder
public class Item : ScriptableObject // Allows this to be considered a scriptable object
{
    public enum ItemType
    {
        weapon,
        armor,
        consumable,
        key,
        tool
    }

    // The Effect that the item has upon usage/equipment 
    public enum Effect
    {
        changeStats,
        giveExp,
        changeDamageType,
        giveBuff
    }

    // This will be moved to its own class (probably called Damage)
    public enum DamageType
    {
        physical,
        fire,
        water,
        earth,
        wind,
        thunder,
        poison,
        life,
        deadly,
        ultimate
    }

    // This will be moved to its own class called Buff
    public enum Buff
    {
        haste,
        slow,
        ladyLuck,
    }


    public string itemName; // The name of the Item in-game
    public Sprite icon; // The icon of the item that will display on the UI
    [TextArea(3,10)] // Allows for a block of text in the editor
    public string description; // The description of the item

    // Determines whether or not the item can be equipped or used 
    public bool equippable; 
    public bool usable;
    public bool droppable;

    [HideInInspector]
    public bool equipped = false;
    
    public ItemType type; // What type will the Item be?    

    public Effect effect; // What effect will the Item have?

    // The following variables are conditionally null depending on the choice of effect (thus they are all hidden from the Inspector)
    [HideInInspector]
    public int exp; // If giveExp is chosen
    [HideInInspector]
    public Stats stats; // If changeStats is chosen
    [HideInInspector]
    public DamageType damageType; // If changeDamageType is chosen
    [HideInInspector]
    public Buff buff; // If giveBuff is chosen

    public void Use(GameObject user) // The function to be called when the item is used by the user
    {
        if(effect == Effect.changeStats) // If the effect of the item is to change the stats then:
        {
            //user.GetComponent<Player>().stats = 
        }

    }

    public void Equip(GameObject owner) // The function to be called when the item is equipped by the owner
    {
        Debug.Log("Equipped " + itemName);
        equipped = true;
        if(effect == Effect.changeStats)
        {
            owner.GetComponent<Entity>().stats.AddStats(stats);
        }
    }

    public void Unequip(GameObject owner)
    {
        Debug.Log("Unequipped " + itemName);
        equipped = false;
        if(effect == Effect.changeStats)
        {
            owner.GetComponent<Entity>().stats.RemoveStats(stats);
        }
    }
}

/* TODOS:

*/