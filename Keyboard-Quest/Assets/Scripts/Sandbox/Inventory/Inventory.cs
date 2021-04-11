/* 
# Author: Filippos Kontogiannis
# Description: The class for the ItemSlots seen in the InventoryUI gameobject
# Editors: ...
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessary for working with UI

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

    private string[] itemTypes; // The pre-defined possible categories of items
    private int index; // The index of the current category being shown in the UI

    // In-Game display =====================================================

    // A quick reference to the items for each slot on the player
    public Item mainHandItem; 
    public Item offHandItem;
    // public Item toolItem;
    // public Item headItem;
    // public Item shouldersItem;
    // public Item chestItem;
    // public Item waistItem;
    // public Item legsItem;
    // public Item feetItem;
    // public Item ringItem;

    // The frames that are shown in the in-game GUI 
    public Image mainHandFrame;
    public Image offHandFrame;
    //public Image toolFrame;
    
    // The actual sprite that is shown on the player model
    private SpriteRenderer mainHandSprite;
    private SpriteRenderer offHandSprite;
    // private SpriteRenderer toolSprite;
    // private SpriteRenderer headItem;


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

    private ItemSlot mainHandSlot;
    private ItemSlot offHandSlot;
    

    
    
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
        mainHandSprite = this.gameObject.transform.Find("MainHand").GetComponent<SpriteRenderer>();
        mainHandSprite.enabled = false;
        offHandSprite = this.gameObject.transform.Find("OffHand").GetComponent<SpriteRenderer>();
        offHandSprite.enabled = false;

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

        mainHandSlot = inventoryUI.transform.Find("PlayerPreview").transform.Find("PlayerImage").transform.Find("ItemColumn0").transform.Find("MainHandSlot").GetComponent<ItemSlot>();
        offHandSlot = inventoryUI.transform.Find("PlayerPreview").transform.Find("PlayerImage").transform.Find("ItemColumn1").transform.Find("OffHandSlot").GetComponent<ItemSlot>();

        UpdateStats(); 
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
                if(mainHandItem == null)
                {
                    EquipMainHand(highlighted);
                }
                else if(offHandItem == null)
                {
                    EquipOffHand(highlighted);
                }
                else
                {
                    offHandItem.Unequip(this.gameObject);
                    EquipOffHand(highlighted);
                }
                unequip.interactable = true;
                equip.interactable = false;
            }
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
                if(mainHandItem == highlighted)
                {
                    UnequipMainHand();
                }
                else if(offHandItem == highlighted)
                {
                    UnequipOffHand();
                }

            }
            
            unequip.interactable = false;
            equip.interactable = true;
        }
        UpdateStats();
    }

    private void UnequipMainHand()
    {
        mainHandItem.Unequip(this.gameObject);
        mainHandItem = null;
        mainHandFrame.enabled = false;
        mainHandSprite.enabled = false;

        mainHandSlot.item = null;
        mainHandSlot.img.color = new Color32(0,0,0,255);
        mainHandSlot.img.sprite = null;

    }

    private void UnequipOffHand()
    {
        offHandItem.Unequip(this.gameObject);
        offHandItem = null;

        offHandFrame.enabled = false;
        offHandSprite.enabled = false;
                    
        offHandSlot.item = null;
        offHandSlot.img.color = new Color32(0,0,0,255);
        offHandSlot.img.sprite = null;
    }

    private void EquipMainHand(Item item)
    {
        mainHandItem = item;
        mainHandItem.Equip(this.gameObject);

        mainHandFrame.enabled = true;
        mainHandFrame.sprite = mainHandItem.icon;

        mainHandSprite.sprite = mainHandItem.icon;
        mainHandSprite.enabled = true;

        mainHandSlot.item = mainHandItem;
        mainHandSlot.img.sprite = mainHandItem.icon;
        mainHandSlot.img.color = new Color32(255,255,255,255);
    }

    private void EquipOffHand(Item item)
    {
        offHandItem = item;
        offHandItem.Equip(this.gameObject);

        offHandFrame.enabled = true;
        offHandFrame.sprite = offHandItem.icon;

        offHandSprite.sprite = offHandItem.icon;
        offHandSprite.enabled = true;

        offHandSlot.item = offHandItem;
        offHandSlot.img.sprite = offHandItem.icon;
        offHandSlot.img.color = new Color32(255,255,255,255);
    }

    public void UseHighlightedItem()
    {

    }

    public void DropHighlightedItem()
    {
        
    }

    private void UpdateStats()
    {

        Player player = this.gameObject.GetComponent<Player>();
        Transform statsBar = inventoryUI.transform.Find("PlayerPreview").transform.Find("Stats");

        //HP
        statsBar.transform.Find("HP").transform.Find("HPValue").GetComponent<Text>().text = player.HP + "/" + player.maxHP;
        RectTransform hpBarFull = statsBar.transform.Find("HP").transform.Find("HPBar").transform.Find("HPBarFull").GetComponent<RectTransform>();
        RectTransform hpBar = statsBar.transform.Find("HP").transform.Find("HPBar").GetComponent<RectTransform>();
        hpBarFull.sizeDelta = new Vector2(((float)player.HP / (float)player.maxHP) * hpBar.sizeDelta.x, hpBar.sizeDelta.y);
        
        //MP
        statsBar.transform.Find("MP").transform.Find("MPValue").GetComponent<Text>().text = player.MP + "/" + player.maxMP;
        RectTransform mpBarFull = statsBar.transform.Find("MP").transform.Find("MPBar").transform.Find("MPBarFull").GetComponent<RectTransform>();
        RectTransform mpBar = statsBar.transform.Find("MP").transform.Find("MPBar").GetComponent<RectTransform>();
        mpBarFull.sizeDelta = new Vector2(((float)player.MP / (float)player.maxMP) * mpBar.sizeDelta.x, mpBar.sizeDelta.y);
        //EXP
        statsBar.transform.Find("EXP").transform.Find("EXPValue").GetComponent<Text>().text = player.EXP + "/" + player.maxEXP;
        RectTransform expBarFull = statsBar.transform.Find("EXP").transform.Find("EXPBar").transform.Find("EXPBarFull").GetComponent<RectTransform>();
        RectTransform expBar = statsBar.transform.Find("EXP").transform.Find("EXPBar").GetComponent<RectTransform>();
        expBarFull.sizeDelta = new Vector2(((float)player.EXP / (float)player.maxEXP) * expBar.sizeDelta.x, expBar.sizeDelta.y);
        //RES
        statsBar.transform.Find("RES").transform.Find("RESValue").GetComponent<Text>().text = player.stats.RES + "/" + Stats.MAX_STAT;
        RectTransform resBarFull = statsBar.transform.Find("RES").transform.Find("RESBar").transform.Find("RESBarFull").GetComponent<RectTransform>();
        RectTransform resBar = statsBar.transform.Find("RES").transform.Find("RESBar").GetComponent<RectTransform>();
        resBarFull.sizeDelta = new Vector2(((float)player.stats.RES / (float)Stats.MAX_STAT) * expBar.sizeDelta.x, expBar.sizeDelta.y);
        //STR
        statsBar.transform.Find("STR").transform.Find("STRValue").GetComponent<Text>().text = player.stats.STR + "/" + Stats.MAX_STAT;
        RectTransform strBarFull = statsBar.transform.Find("STR").transform.Find("STRBar").transform.Find("STRBarFull").GetComponent<RectTransform>();
        RectTransform strBar = statsBar.transform.Find("STR").transform.Find("STRBar").GetComponent<RectTransform>();
        strBarFull.sizeDelta = new Vector2(((float)player.stats.STR / (float)Stats.MAX_STAT) * expBar.sizeDelta.x, expBar.sizeDelta.y);
        //DEX
        statsBar.transform.Find("DEX").transform.Find("DEXValue").GetComponent<Text>().text = player.stats.DEX + "/" + Stats.MAX_STAT;
        RectTransform dexBarFull = statsBar.transform.Find("DEX").transform.Find("DEXBar").transform.Find("DEXBarFull").GetComponent<RectTransform>();
        RectTransform dexBar = statsBar.transform.Find("DEX").transform.Find("DEXBar").GetComponent<RectTransform>();
        dexBarFull.sizeDelta = new Vector2(((float)player.stats.DEX / (float)Stats.MAX_STAT) * expBar.sizeDelta.x, expBar.sizeDelta.y);
        //WIT
        statsBar.transform.Find("WIT").transform.Find("WITValue").GetComponent<Text>().text = player.stats.WIT + "/" + Stats.MAX_STAT;
        RectTransform witBarFull = statsBar.transform.Find("WIT").transform.Find("WITBar").transform.Find("WITBarFull").GetComponent<RectTransform>();
        RectTransform witBar = statsBar.transform.Find("WIT").transform.Find("WITBar").GetComponent<RectTransform>();
        witBarFull.sizeDelta = new Vector2(((float)player.stats.WIT / (float)Stats.MAX_STAT) * expBar.sizeDelta.x, expBar.sizeDelta.y);
        //LCK
        statsBar.transform.Find("LCK").transform.Find("LCKValue").GetComponent<Text>().text = player.stats.LCK + "/" + Stats.MAX_STAT;
        RectTransform lckBarFull = statsBar.transform.Find("LCK").transform.Find("LCKBar").transform.Find("LCKBarFull").GetComponent<RectTransform>();
        RectTransform lckBar = statsBar.transform.Find("LCK").transform.Find("LCKBar").GetComponent<RectTransform>();
        lckBarFull.sizeDelta = new Vector2(((float)player.stats.LCK / (float)Stats.MAX_STAT) * expBar.sizeDelta.x, expBar.sizeDelta.y);
        //FTH
        statsBar.transform.Find("FTH").transform.Find("FTHValue").GetComponent<Text>().text = player.stats.FTH + "/" + Stats.MAX_STAT;
        RectTransform fthBarFull = statsBar.transform.Find("FTH").transform.Find("FTHBar").transform.Find("FTHBarFull").GetComponent<RectTransform>();
        RectTransform fthBar = statsBar.transform.Find("FTH").transform.Find("FTHBar").GetComponent<RectTransform>();
        fthBarFull.sizeDelta = new Vector2(((float)player.stats.FTH / (float)Stats.MAX_STAT) * expBar.sizeDelta.x, expBar.sizeDelta.y);
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
