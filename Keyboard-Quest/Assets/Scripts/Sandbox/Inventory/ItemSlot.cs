/* 
# Author: Filippos Kontogiannis
# Description: The class for the ItemSlots seen in the InventoryUI gameobject
# Editors: ...
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessary for working with UI

public class ItemSlot : MonoBehaviour
{
    [HideInInspector]
    public Item item; // The item that the gameobject is holding
    [HideInInspector]
    public Image img; // The img that the gameobject should display
    [HideInInspector]
    public SpriteRenderer sprite; // The main img  
    [HideInInspector]
    public Image frame; // The in game frame of main/off/tool hand items

    void Start() 
    {
        img = GetComponent<Image>(); // Get the Image component of the gameobject this is attached to
    }

    public void ShowItemDescription() // Call this function every time the player clicks on the item slot
    {
        GameObject.Find("Player").GetComponent<Inventory>().ShowItemDescription(item); // Show the description of the slot's item
    }
}

/* TODOS:

*/