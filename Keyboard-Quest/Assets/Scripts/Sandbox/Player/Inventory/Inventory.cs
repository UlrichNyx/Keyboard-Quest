using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[] items;

    private Item[] weaponItems;
    private int lastWeaponIndex;
    private Item[] armorItems;
    private int lastArmorIndex;
    private Item[] consumables;
    private int lastConsumableIndex;
    private Item[] keyItems;
    private int lastKeyIndex;
    private Item[] toolItems;
    private int lastToolIndex;

    private Dictionary<string, Item[]> allItems;
    private Dictionary<string, int> allIndices;

    private string[] itemTypes;
    private int index;

    // In-Game display
    public Item mainHandItem;
    //public Item offHandItem;
    //public Item toolItem;
    public Image mainHandFrame;
    //public Image offHandFrame;
    //public Image toolFrame;
    
    // Inventory window
    public GameObject inventoryUI;

    // Item Grid
    private ItemSlot[] itemSlots;
    private Image itemIconDisplay;
    private Text itemNameDisplay;
    private Text itemDescriptionDisplay;

    private Text itemTypeDisplay;
    private Item highlighted;

    private Button equip;
    private Button unequip;
    private Button use;
    private Button drop;

    // Player Display
    

    private SpriteRenderer mainHandSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        equip = inventoryUI.transform.Find("ItemsDisplay").transform.Find("Equip").GetComponent<Button>();
        unequip = inventoryUI.transform.Find("ItemsDisplay").transform.Find("Unequip").GetComponent<Button>();
        drop = inventoryUI.transform.Find("ItemsDisplay").transform.Find("Drop").GetComponent<Button>();
        use = inventoryUI.transform.Find("ItemsDisplay").transform.Find("Use").GetComponent<Button>();

        equip.interactable = false;
        unequip.interactable= false;
        use.interactable = false;
        drop.interactable = false;


        weaponItems = new Item[50];
        armorItems = new Item[50];
        consumables = new Item[50];
        keyItems = new Item[50];
        toolItems = new Item[50];

        lastWeaponIndex = 0;
        lastArmorIndex = 0;
        lastConsumableIndex = 0;
        lastKeyIndex = 0;
        lastToolIndex = 0;

        allItems = new Dictionary<string, Item[]>();
        allIndices = new Dictionary<string, int>();
        
        index = 0;
        itemTypes = new string[5]{"Weapons", "Armor", "Consumables", "Key Items", "Tools"};

        allItems.Add(itemTypes[0], weaponItems);
        allItems.Add(itemTypes[1], armorItems);
        allItems.Add(itemTypes[2], consumables);
        allItems.Add(itemTypes[3], keyItems);
        allItems.Add(itemTypes[4], toolItems);

        allIndices.Add(itemTypes[0], lastWeaponIndex);
        allIndices.Add(itemTypes[1], lastArmorIndex);
        allIndices.Add(itemTypes[2], lastConsumableIndex);
        allIndices.Add(itemTypes[3], lastKeyIndex);
        allIndices.Add(itemTypes[4], lastToolIndex);

    
        mainHandItem = items[0];
        mainHandFrame.enabled = false;
        mainHandSprite = this.gameObject.transform.Find("MainHand").GetComponent<SpriteRenderer>();
        mainHandSprite.enabled = false;

        foreach (Item item in items)
        {
            if(item == null)
            {
                continue;
            }
            if(item.type == Item.ItemType.weapon)
            {
                weaponItems[lastWeaponIndex] = item;
                lastWeaponIndex += 1;
            }
            if(item.type == Item.ItemType.armor)
            {
                armorItems[lastArmorIndex] = item;
                lastArmorIndex += 1;
            }
            if(item.type == Item.ItemType.consumable)
            {
                consumables[lastConsumableIndex] = item;
                lastConsumableIndex += 1;
            }
            if(item.type == Item.ItemType.key)
            {
                keyItems[lastKeyIndex] = item;
                lastKeyIndex += 1;
            }
            if(item.type == Item.ItemType.tool)
            {
                toolItems[lastToolIndex] = item;
                lastToolIndex += 1;
            }
        }
        
        int counter = 0;
        
        itemSlots = new ItemSlot[50];
        foreach(Transform child in inventoryUI.transform.Find("ItemsDisplay").transform.Find("ItemsGrid"))
        {
            foreach(Transform c in child.transform)
            {
                itemSlots[counter] = c.GetComponent<ItemSlot>();
                if(weaponItems[counter] != null)
                {
                    itemSlots[counter].img.color = new Color32(255,255,255,255);
                    itemSlots[counter].img.sprite = weaponItems[counter].icon;
                    itemSlots[counter].item = weaponItems[counter];
                }
                counter += 1;
            }
        }

        itemIconDisplay = inventoryUI.transform.Find("ItemsDisplay").transform.Find("ItemIcon").GetComponent<Image>();
        itemNameDisplay = inventoryUI.transform.Find("ItemsDisplay").transform.Find("ItemNameText").GetComponent<Text>();
        itemDescriptionDisplay = inventoryUI.transform.Find("ItemsDisplay").transform.Find("ItemDescriptionText").GetComponent<Text>();
        itemTypeDisplay = inventoryUI.transform.Find("ItemsDisplay").transform.Find("ItemTypeText").GetComponent<Text>();
        itemTypeDisplay.text = itemTypes[index];
        inventoryUI.SetActive(false);
    }

    void ReloadItemSlots(Item[] currentItems)
    {
        int counter = 0;
        foreach(Item item in currentItems)
        {
            if(item != null)
            {
                itemSlots[counter].img.color = new Color32(255,255,255,255);
                itemSlots[counter].img.sprite = currentItems[counter].icon;
                itemSlots[counter].item = currentItems[counter];
            }
            else
            {
                itemSlots[counter].img.color = new Color32(0,0,0,255);
                itemSlots[counter].img.sprite = null;
                itemSlots[counter].item = null;
            }
            counter += 1;
        }
    }

    public void DisplayNextItemType()
    {
        index += 1;
        if(index > itemTypes.Length - 1)
        {
            index = 0;
        }
        itemTypeDisplay.text = itemTypes[index];
        ReloadItemSlots(allItems[itemTypes[index]]);
    }

    public void DisplayPreviousItemType()
    {
        index -= 1;
        if(index < 0)
        {
            index = itemTypes.Length - 1;
        }
        itemTypeDisplay.text = itemTypes[index];
        ReloadItemSlots(allItems[itemTypes[index]]);
    }

    public void ShowItemDescription(Item item)
    {
        equip.interactable = false;
        unequip.interactable = false;
        use.interactable = false;
        drop.interactable = false;
        if(item == null)
        {
            highlighted = null;
            itemIconDisplay.color = new Color32(0,0,0,255);
            itemIconDisplay.sprite = null;
            itemNameDisplay.text = "Item Name";
            itemDescriptionDisplay.text = "Item Description";
            return;
        }

        highlighted = item;
        if(highlighted.equippable)
        {
            equip.interactable = true;
        }
        if(highlighted.equipped)
        {
            unequip.interactable = true;
            equip.interactable = false;
        }
        if(highlighted.usable)
        {
            use.interactable = true;
        }
        if(highlighted.droppable)
        {
            drop.interactable = true;
        }
        itemIconDisplay.color = new Color32(255,255,255,255);
        itemIconDisplay.sprite = item.icon;
        itemNameDisplay.text = item.itemName;
        itemDescriptionDisplay.text = item.description;
    }
    
    public void EquipHighlightedItem()
    {
        if(highlighted == null)
        {
            Debug.Log("No item selected!");
        }
        if(!highlighted.equipped)
        {
            highlighted.Equip(this.gameObject);
            mainHandItem = highlighted;
            mainHandItem.Equip(this.gameObject);
            mainHandFrame.sprite = mainHandItem.icon;
            mainHandFrame.enabled = true;
            mainHandSprite.enabled = true;
            mainHandSprite.sprite = mainHandItem.icon;
            unequip.interactable = true;
            equip.interactable = false;
        }
    }

    public void UnequipHighlightedItem()
    {
        if(highlighted == null)
        {
            Debug.Log("No item selected!");
        }
        if(highlighted.equipped)
        {
            highlighted.Unequip(this.gameObject);
            mainHandItem = highlighted;
            mainHandItem.Unequip(this.gameObject);
            mainHandFrame.enabled = false;
            mainHandSprite.enabled = false;
            unequip.interactable = false;
            equip.interactable = true;
        }
    }

    public void UseHighlightedItem()
    {

    }

    public void DropHighlightedItem()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && !inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(false);
        }
    }
}
