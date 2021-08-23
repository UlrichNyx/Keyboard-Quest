using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    private GameObject[] itemButtons;
    public ItemListing[] itemsOnSale;
    private Item highlighted;

    public void ShowItemDescription(Item item)
    {
        highlighted = item;

        Image itemIconDisplay = shopUI.transform.Find("ItemIcon").GetComponent<Image>();
        Text itemNameDisplay = shopUI.transform.Find("ItemNameText").GetComponent<Text>();
        Text itemDescriptionDisplay = shopUI.transform.Find("ItemDescriptionText").GetComponent<Text>();

        itemIconDisplay.color = new Color32(255,255,255,255);
        itemIconDisplay.sprite = item.icon;
        itemNameDisplay.text = item.itemName;
        itemDescriptionDisplay.text = item.description;
    }

    public void ShowAffirmative()
    {
        Transform list = shopUI.transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content");
        int i = 0;
        foreach (Transform child in list)
        {
            if(i < itemsOnSale.Length)
            {
                child.GetComponent<ItemRow>().item = itemsOnSale[i].item;
                child.Find("Button").transform.Find("ItemText").GetComponent<Text>().text = itemsOnSale[i].item.itemName;
                child.Find("Button").transform.Find("ItemImage").GetComponent<Image>().sprite = itemsOnSale[i].item.icon;
                child.Find("Button").transform.Find("PriceText").GetComponent<Text>().text = itemsOnSale[i].price + "g";
            }
            i++;
        }
        shopUI.SetActive(true);
    }

    public void ShowNegative()
    {
        shopUI.SetActive(false);
    }
}
