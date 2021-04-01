using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [HideInInspector]
    public Item item;
    [HideInInspector]
    public Image img;

    void Start() 
    {
        img = GetComponent<Image>();
    }

    public void ShowItemDescription()
    {
        GameObject.Find("Player").GetComponent<Inventory>().ShowItemDescription(item);
    }
}
