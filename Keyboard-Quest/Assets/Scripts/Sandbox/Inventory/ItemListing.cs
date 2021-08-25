/* 
# Author: Filippos Kontogiannis
# Description: The base class of an Item Listing in a shop
# Editors: ...
*/

/* TODOS:

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemListing
{
    public Item item; // The item being sold
    public int price; // The price the vendor asks for
}
