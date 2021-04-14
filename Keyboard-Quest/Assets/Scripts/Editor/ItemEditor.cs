/* 
# Author: Filippos Kontogiannis
# Description: The class for defining the custom Item Editor that makes it really easy to make new items!
# Editors: ...
*/

// To create a new item: right click on project folder > Inventory > Item > Fill out the information displayed :))

using UnityEngine;
using UnityEditor; // Necessary for working with editors

[CustomEditor(typeof(Item))] // Determines the type of object this editor is made for
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Display everything as you normally would (base) But: 
        Item item = (Item)target; // target refers to the object being inspected, cast this object to be of type Item

        if(item.effect == Item.Effect.changeStats) // If the chosen effect is for changing stats, display an editor for stats
        {
            item.stats.RES = EditorGUILayout.IntSlider("Resilience",item.stats.RES, 0, 100);
            item.stats.STR = EditorGUILayout.IntSlider("Strength",item.stats.STR, 0, 100);
            item.stats.DEX = EditorGUILayout.IntSlider("Dexterity",item.stats.DEX , 0, 100);
            item.stats.WIT = EditorGUILayout.IntSlider("Wit",item.stats.WIT, 0, 100);
            item.stats.LCK = EditorGUILayout.IntSlider("Luck",item.stats.LCK, 0, 100);
            item.stats.FTH = EditorGUILayout.IntSlider("Faith",item.stats.FTH, 0, 100);
        }
        else if(item.effect == Item.Effect.giveExp) // If the chosen effect is for giving experience, show a slider for experience gains
        {
            item.exp = EditorGUILayout.IntSlider("Experience",item.exp, 0, 1000);
        }

        else if(item.effect == Item.Effect.changeDamageType) // Show the different types of damage
        {
            item.damageType = (Item.DamageType)EditorGUILayout.EnumPopup("Damage Type", item.damageType);
        }
        else if(item.effect == Item.Effect.giveBuff) // Show the different buffs one can gain
        {
            item.buff = (Item.Buff)EditorGUILayout.EnumPopup("Buff", item.buff);
        }

        if(item.type == Item.ItemType.armor)
        {
            item.armorType = (Item.ArmorType)EditorGUILayout.EnumPopup("Armor Type", item.armorType);
        }
    }

}

/* TODOS:

*/