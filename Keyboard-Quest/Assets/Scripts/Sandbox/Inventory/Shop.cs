/* 
# Author: Filippos Kontogiannis
# Description: The class which handles shopping
# Editors: ...
*/

/* TODOS:

*/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Shop : MonoBehaviour
{
    public GameObject shopUI; // The reference to the shop's UI
    public ItemListing[] itemsOnSale; // A reference to the items that are on sale 
    private Item highlighted; // The current item that is being inspected

    public void ShowItemDescription(Item item) // Show the item description of the specific item
    {
        highlighted = item; // Set the current item to the one in the parameters

        Image itemIconDisplay = shopUI.transform.Find("ItemIcon").GetComponent<Image>(); // Get the Image object
        Text itemNameDisplay = shopUI.transform.Find("ItemNameText").GetComponent<Text>(); // Get the name Text object
        Text itemDescriptionDisplay = shopUI.transform.Find("ItemDescriptionText").GetComponent<Text>(); // Get the description Text object

        itemIconDisplay.color = new Color32(255,255,255,255); // Set the image to white
        itemIconDisplay.sprite = item.icon; // Set the sprite 
        itemNameDisplay.text = item.itemName; // Set the name
        itemDescriptionDisplay.text = item.description; // Set the description
    }

    public void BuyHighlightedItem()
    {
        if(highlighted == null)
        {
            return;
        }
        foreach(ItemListing itemList in itemsOnSale)
        {
            if(itemList.item == highlighted)
            {
                if(FindObjectOfType<Player>().currency >= itemList.price)
                {
                    FindObjectOfType<Player>().currency -= itemList.price;
                    FindObjectOfType<Player>().GetComponent<Inventory>().AddItem(highlighted);
                    FindObjectOfType<AudioManager>().Play("Gold");
                    shopUI.transform.Find("SystemMessage").GetComponent<Text>().text = "Your gold: " + FindObjectOfType<Player>().currency + "g";
                    return;
                }
            }
        }
    }

    public void ShowBuyingWindow() // If the choice to buy was picked
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
        FindObjectOfType<Player>().GetComponent<Player>().currentState = PlayerState.shopping;
        // The shop's list of items gameobject
        Transform list = shopUI.transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content");

        int i = 0; // Counter used to iterate through the items in itemsOnSale
        foreach (Transform child in list) // For each panel in the shop
        {
            if(i < itemsOnSale.Length) // If the items to sell have not ran out
            {
                // Set the specific Item Row to this item
                child.GetComponent<ItemRow>().item = itemsOnSale[i].item; 
                child.Find("Button").transform.Find("ItemText").GetComponent<Text>().text = itemsOnSale[i].item.itemName;
                child.Find("Button").transform.Find("ItemImage").GetComponent<Image>().sprite = itemsOnSale[i].item.icon;
                child.Find("Button").transform.Find("PriceText").GetComponent<Text>().text = itemsOnSale[i].price + "g";
            }
            i++;
        }
        shopUI.transform.Find("BuyButton").GetComponent<Button>().interactable = true;
        shopUI.transform.Find("SellButton").GetComponent<Button>().interactable = false;
        shopUI.transform.Find("SystemMessage").GetComponent<Text>().text = "Your gold: " + FindObjectOfType<Player>().currency + "g";

        shopUI.SetActive(true); // Turn on the shop's UI
    }

    public void ShowSellingWindow() // If the choice to sell was picked
    {
        // TODO
        FindObjectOfType<DialogueManager>().EndDialogue();
        FindObjectOfType<Player>().GetComponent<Player>().currentState = PlayerState.shopping;
        shopUI.SetActive(true); // Turn off the shop's UI
        shopUI.transform.Find("BuyButton").GetComponent<Button>().interactable = false;
        shopUI.transform.Find("SellButton").GetComponent<Button>().interactable = true;
    }

    public void HideBuyingWindow()
    {
        shopUI.SetActive(false);
        this.GetComponent<QuestionTrigger>().StartReply();
        FindObjectOfType<Player>().GetComponent<Player>().currentState = PlayerState.interact;
    }
}
