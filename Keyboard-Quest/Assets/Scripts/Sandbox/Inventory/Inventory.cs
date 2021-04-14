/* 
# Author: Filippos Kontogiannis
# Description: The class for the ItemSlots seen in the InventoryUI gameobject
# Editors: ...
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessary for working with UI
using System;

public class Inventory : MonoBehaviour
{
    public Item[] items; // All the items the player has in their inventory, note that these are visible from the inspector

    // The index variables below depic the last index in comparison to the total availability of the arrays (so a number from 0 to 49)
    private Item[] weaponItems; // The subset of items that are weapons (can be held in the mainhand/offhand)
    private int lastWeaponIndex;
    private Item[] armorItems; // The subset of items that can be worn (can be equipped in the armor slots)
    private int lastArmorIndex;
    private Item[] consumables; // The subset of items that can be used
    private int lastConsumableIndex;
    private Item[] keyItems; // The subset of items that are important for some quest (and therefore can't be dropped)
    private int lastKeyIndex;
    private Item[] toolItems; // The subset of items that can be used as tools
    private int lastToolIndex;

    // Note that these index variables also represent how many items of a certain type the player holds

    private Dictionary<string, Item[]> allItems; // The link between the category name and the subsets themselves
    private Dictionary<string, int> allIndices; // The link between the subsets and their respective last index

    private Dictionary<Item.ArmorType, ItemSlot> armorSlots;

    private string[] itemTypes; // The pre-defined possible categories of items
    private int index; // The index of the current category being shown in the UI

    // In-Game display =====================================================

    // The frames that are shown in the in-game GUI 
    public Image mainHandFrame, offHandFrame, toolFrame;

    // Inventory window
    public GameObject inventoryUI;

    // Item Grid
    private ItemSlot[] itemSlots;
    private Image itemIconDisplay;
    private Text itemNameDisplay;
    private Text itemDescriptionDisplay;

    private Text itemTypeDisplay;
    private Item highlighted;

    private Button equip, unequip, use, drop;

    // Player Display
    private ItemSlot mainHandSlot, offHandSlot, toolSlot, headSlot, shoulderSlot, chestSlot, waistSlot, legsSlot, feetSlot, ringSlot;
    private Text levelLabel;


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

        mainHandFrame.enabled = false;
        offHandFrame.enabled = false;
        toolFrame.enabled = false;

        ReloadItemCategories();
        
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


        // Player preview


        // Left Column
        Transform column0 = inventoryUI.transform.Find("PlayerPreview").transform.Find("PlayerImage").transform.Find("ItemColumn0").transform;
        headSlot = column0.Find("HeadSlot").GetComponent<ItemSlot>();
        shoulderSlot = column0.Find("ShoulderSlot").GetComponent<ItemSlot>();
        chestSlot = column0.Find("ChestSlot").GetComponent<ItemSlot>();
        waistSlot = column0.Find("WaistSlot").GetComponent<ItemSlot>();
        mainHandSlot = column0.Find("MainHandSlot").GetComponent<ItemSlot>();

        // Right Column
        Transform column1 = inventoryUI.transform.Find("PlayerPreview").transform.Find("PlayerImage").transform.Find("ItemColumn1").transform;
        legsSlot = column1.Find("LegsSlot").GetComponent<ItemSlot>();
        feetSlot = column1.Find("FeetSlot").GetComponent<ItemSlot>();
        ringSlot = column1.Find("RingSlot").GetComponent<ItemSlot>();
        toolSlot = column1.Find("ToolSlot").GetComponent<ItemSlot>();
        offHandSlot = column1.Find("OffHandSlot").GetComponent<ItemSlot>();

        // Level Display
        levelLabel = inventoryUI.transform.Find("PlayerPreview").transform.Find("Stats").transform.Find("LVL").GetComponent<Text>();
        UpdateStats(); 


        headSlot.sprite = this.gameObject.transform.Find("Head").GetComponent<SpriteRenderer>();
        headSlot.sprite.enabled = false;
        mainHandSlot.sprite = this.gameObject.transform.Find("MainHand").GetComponent<SpriteRenderer>();
        mainHandSlot.sprite.enabled = false;
        mainHandSlot.frame = mainHandFrame;
        offHandSlot.sprite = this.gameObject.transform.Find("OffHand").GetComponent<SpriteRenderer>();
        offHandSlot.sprite.enabled = false;
        offHandSlot.frame = offHandFrame;
        toolSlot.sprite = this.gameObject.transform.Find("Tool").GetComponent<SpriteRenderer>();
        toolSlot.sprite.enabled = false;
        toolSlot.frame = toolFrame;
        shoulderSlot.sprite = this.gameObject.transform.Find("Shoulder").GetComponent<SpriteRenderer>();
        shoulderSlot.sprite.enabled = false;
        chestSlot.sprite = this.gameObject.transform.Find("Chest").GetComponent<SpriteRenderer>();
        chestSlot.sprite.enabled = false;
        waistSlot.sprite = this.gameObject.transform.Find("Waist").GetComponent<SpriteRenderer>();
        waistSlot.sprite.enabled = false;
        legsSlot.sprite = this.gameObject.transform.Find("Legs").GetComponent<SpriteRenderer>();
        legsSlot.sprite.enabled = false;
        feetSlot.sprite = this.gameObject.transform.Find("Feet").GetComponent<SpriteRenderer>();
        feetSlot.sprite.enabled = false;
        ringSlot.sprite = this.gameObject.transform.Find("Ring").GetComponent<SpriteRenderer>();
        ringSlot.sprite.enabled = false;

        armorSlots = new Dictionary<Item.ArmorType, ItemSlot>();
        armorSlots.Add(Item.ArmorType.head, headSlot);
        armorSlots.Add(Item.ArmorType.shoulder, shoulderSlot);
        armorSlots.Add(Item.ArmorType.chest, chestSlot);
        armorSlots.Add(Item.ArmorType.waist, waistSlot);
        armorSlots.Add(Item.ArmorType.legs, legsSlot);
        armorSlots.Add(Item.ArmorType.feet, feetSlot);
        armorSlots.Add(Item.ArmorType.ring, ringSlot);
    }

    void ReloadItemCategories()
    {
        lastWeaponIndex = 0;
        lastArmorIndex = 0;
        lastConsumableIndex = 0;
        lastKeyIndex = 0;
        lastToolIndex = 0;

        Array.Clear(weaponItems, 0, weaponItems.Length);
        Array.Clear(armorItems, 0, weaponItems.Length);
        Array.Clear(consumables, 0, weaponItems.Length);
        Array.Clear(keyItems, 0, weaponItems.Length);
        Array.Clear(toolItems, 0, weaponItems.Length);

        foreach (Item item in items)
        { 
            if(item == null)
            {
                continue;
            }
            // Temporary fix
            item.equipped = false;
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
            if(highlighted.type == Item.ItemType.weapon)
            {
                if(mainHandSlot.item == null)
                {
                    mainHandSlot.item = EquipSlot(highlighted, mainHandSlot);
                }
                else if(offHandSlot.item == null)
                {
                    offHandSlot.item = EquipSlot(highlighted, offHandSlot);
                }
                else
                {
                    offHandSlot.item.Unequip(this.gameObject);
                    offHandSlot.item = EquipSlot(highlighted, offHandSlot);
                }
            }
            else if(highlighted.type == Item.ItemType.armor)
            {
                if(armorSlots[highlighted.armorType].item == null)
                {
                    armorSlots[highlighted.armorType].item = EquipSlot(highlighted, armorSlots[highlighted.armorType]);
                }
                else
                {
                    armorSlots[highlighted.armorType].item.Unequip(this.gameObject);
                    armorSlots[highlighted.armorType].item = EquipSlot(highlighted, armorSlots[highlighted.armorType]);
                }
            }
            else if(highlighted.type == Item.ItemType.tool)
            {
                if(toolSlot.item == null)
                {
                    toolSlot.item = EquipSlot(highlighted, toolSlot);
                }
                else
                {
                    toolSlot.item.Unequip(this.gameObject);
                    toolSlot.item = EquipSlot(highlighted, toolSlot);
                }
            }
            unequip.interactable = true;
            equip.interactable = false;
        }

        UpdateStats();
    }

    public void UnequipHighlightedItem()
    {
        if(highlighted == null)
        {
            Debug.Log("No item selected!");
        }
        if(highlighted.equipped)
        {
            if(highlighted.type == Item.ItemType.weapon)
            {
                if(mainHandSlot.item == highlighted)
                {
                    mainHandSlot.item = UnequipSlot(mainHandSlot);
                }
                else if(offHandSlot.item == highlighted)
                {
                    offHandSlot.item = UnequipSlot(offHandSlot);
                }

            }
            else if(highlighted.type == Item.ItemType.armor)
            {
                
                armorSlots[highlighted.armorType].item = UnequipSlot(armorSlots[highlighted.armorType]);
            }
            else if(highlighted.type == Item.ItemType.tool)
            {

                toolSlot.item = UnequipSlot(toolSlot);
            }
            
            unequip.interactable = false;
            equip.interactable = true;
        }
        UpdateStats();
    }

    private Item UnequipSlot(ItemSlot slot)
    {
        slot.item.Unequip(this.gameObject);
        slot.item = null;

        if(slot.frame != null)
        {
            slot.frame.enabled = false;
        }
        slot.sprite.enabled = false;
                    
        slot.img.color = new Color32(0,0,0,255);
        slot.img.sprite = null;

        return slot.item;
    }

    private Item EquipSlot(Item item, ItemSlot slot)
    {
        slot.item = item;
        slot.item.Equip(this.gameObject);

        if(slot.frame != null)
        {
            slot.frame.enabled = true;
            slot.frame.sprite = slot.item.icon;
        }
        
        slot.sprite.sprite = slot.item.icon;
        slot.sprite.enabled = true;

        slot.img.sprite = slot.item.icon;
        slot.img.color = new Color32(255,255,255,255);
        return slot.item;
    }

    public void UseHighlightedItem()
    {
        if(highlighted == null)
        {
            Debug.Log("No item selected!");
        }
        highlighted.Use(this.gameObject);
        if(highlighted.consumed)
        {
            for(int i = 0; i < items.Length; i++) 
            {
                if(items[i] == highlighted)
                {
                    items[i] = null;
                    ReloadItemCategories();
                    ReloadItemSlots(allItems[itemTypes[index]]);
                    ShowItemDescription(null);
                }
            }
            use.interactable = false;
        }
        UpdateStats();
    }

    public void DropHighlightedItem()
    {
        UpdateStats();
    }

    private void UpdateStatBar(string name, Transform statsBar, Player player, int val, int maxVal)
    {
        statsBar.transform.Find(name).transform.Find(name + "Value").GetComponent<Text>().text = val + "/" + maxVal;
        RectTransform barFull = statsBar.transform.Find(name).transform.Find(name + "Bar").transform.Find(name +"BarFull").GetComponent<RectTransform>();
        RectTransform bar = statsBar.transform.Find(name).transform.Find(name +"Bar").GetComponent<RectTransform>();
        barFull.sizeDelta = new Vector2(((float)val / (float)maxVal) * bar.sizeDelta.x, bar.sizeDelta.y);
    }

    private void UpdateStats()
    {
        
        Player player = this.gameObject.GetComponent<Player>();
        Transform statsBar = inventoryUI.transform.Find("PlayerPreview").transform.Find("Stats");

        levelLabel.text = "Lv. " + player.level + "/\n" + player.maxLevel;
        UpdateStatBar("HP", statsBar, player, player.HP, player.maxHP);
        UpdateStatBar("MP", statsBar, player, player.MP, player.maxMP);
        UpdateStatBar("EXP", statsBar, player, player.EXP, player.maxEXP);
        UpdateStatBar("RES", statsBar, player, player.stats.RES, Stats.MAX_STAT);
        UpdateStatBar("STR", statsBar, player, player.stats.STR, Stats.MAX_STAT);
        UpdateStatBar("DEX", statsBar, player, player.stats.DEX, Stats.MAX_STAT);
        UpdateStatBar("WIT", statsBar, player, player.stats.WIT, Stats.MAX_STAT);
        UpdateStatBar("LCK", statsBar, player, player.stats.LCK, Stats.MAX_STAT);
        UpdateStatBar("FTH", statsBar, player, player.stats.FTH, Stats.MAX_STAT);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && !inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(true);
            UpdateStats();
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(false);
        }
    }
}
