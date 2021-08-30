/* 
# Author: Filippos Kontogiannis
# Description: The class for defining the custom Item Editor that makes it really easy to make new items!
# Editors: ...
*/

// To create a new item: right click on project folder > Inventory > Item > Fill out the information displayed :))

using UnityEngine;
using UnityEditor; // Necessary for working with editors

[CustomEditor(typeof(Quest))] // Determines the type of object this editor is made for
public class QuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Display everything as you normally would (base) But: 
        Quest quest = (Quest)target; // target refers to the object being inspected, cast this object to be of type Item
        if(quest.requirement == Quest.Requirement.slay) // If the chosen effect is for giving experience, show a slider for experience gains
        {
            quest.enemyToSlay = (Enemy)EditorGUILayout.ObjectField(quest.enemyToSlay, typeof(Enemy), false);
        }
        else if(quest.requirement == Quest.Requirement.obtain) // If the chosen effect is for giving experience, show a slider for experience gains
        {
            quest.itemToObtain = (Item)EditorGUILayout.ObjectField(quest.itemToObtain, typeof(Item), false);
        }
        else if(quest.requirement == Quest.Requirement.escort) // If the chosen effect is for giving experience, show a slider for experience gains
        {
            //
        }
        if (GUILayout.Button("Save")) // Sometimes the asset is not properly saved so there needs to be an explicit save button
        {        
            AssetDatabase.SaveAssets();
            Debug.Log("Saved " + quest.questName + " in assets!");
        }
    }

}

/* TODOS:

*/