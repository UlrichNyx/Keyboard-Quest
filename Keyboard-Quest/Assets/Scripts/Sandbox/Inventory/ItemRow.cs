/* 
# Author: Filippos Kontogiannis
# Description: The component to be attached to the Item panels in shops
# Editors: ...
*/

/* TODOS:

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRow : MonoBehaviour
{
    public Item item; // The item that the specific panel is refering to

    public void ShowItemDescription() // Show the item description when clicking on the panel
    {
        GameObject.Find("Shopkeeper").GetComponent<Shop>().ShowItemDescription(item);
    }
}
