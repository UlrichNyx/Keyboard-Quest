/* 
# Author: Filippos Kontogiannis
# Description: The class for defining the custom Item Editor that makes it really easy to make new items!
# Editors: ...
*/

// To create a new item: right click on project folder > Inventory > Item > Fill out the information displayed :))

using UnityEngine;
using UnityEditor; // Necessary for working with editors

[CustomEditor(typeof(Enemy))] // Determines the type of object this editor is made for
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Display everything as you normally would (base) But: 
        Enemy enemy = (Enemy)target; // target refers to the object being inspected, cast this object to be of type Item
        
        if (GUILayout.Button("Save")) // Sometimes the asset is not properly saved so there needs to be an explicit save button
        {        
            AssetDatabase.SaveAssets();
            Debug.Log("Saved " + enemy.enemyName + " in assets!");
        }
    }

}

/* TODOS:

*/