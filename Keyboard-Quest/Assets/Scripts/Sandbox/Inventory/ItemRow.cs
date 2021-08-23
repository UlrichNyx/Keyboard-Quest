using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRow : MonoBehaviour
{
    public Item item;

    public void ShowItemDescription()
    {
        GameObject.Find("Shopkeeper").GetComponent<Shop>().ShowItemDescription(item);
    }
}
