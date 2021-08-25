/* 
# Author: Filippos Kontogiannis
# Description: The component used for picking up items
# Editors: ...
*/

/* TODOS:

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item item; // The item that is contained in the pickup
    private SpriteRenderer sprite; // The sprite of the pickup

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>(); // As soon as the game starts, get the sprite component
    }

    public void SetItem(Item item) // When pressing the drop button or simply spawning a pickup, set the item
    {
        this.item = item; // set the item
        sprite.sprite = item.icon; // set the sprite
    }

}
