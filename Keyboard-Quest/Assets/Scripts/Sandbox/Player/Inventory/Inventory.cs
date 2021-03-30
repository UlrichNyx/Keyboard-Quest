using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[] items;
    public Item mainHandItem;
    public Item offHandItem;
    public Item toolItem;

    public Image mainHandFrame;
    public Image offHandFrame;
    public Image toolFrame;
    
    public GameObject inventoryUI;


    private SpriteRenderer mainHandSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        mainHandItem = items[0];
        mainHandFrame.enabled = false;
        mainHandSprite = this.gameObject.transform.Find("MainHand").GetComponent<SpriteRenderer>();
        mainHandSprite.enabled = false;
        

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
        if(Input.GetKeyDown(KeyCode.E) && !mainHandItem.equipped)
        {
            mainHandItem.Equip(this.gameObject);
            mainHandFrame.sprite = mainHandItem.icon;
            mainHandFrame.enabled = true;
            mainHandSprite.enabled = true;
            mainHandSprite.sprite = mainHandItem.icon;
            
        }
        else if(Input.GetKeyDown(KeyCode.E) && mainHandItem.equipped)
        {
            mainHandItem.Unequip(this.gameObject);
            mainHandFrame.enabled = false;
            mainHandSprite.enabled = false;
        }
    }
}
