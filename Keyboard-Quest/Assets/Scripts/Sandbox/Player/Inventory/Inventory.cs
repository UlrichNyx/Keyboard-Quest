using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] items;
    public Item mainHand;
    public Item offHand;
    
    // Start is called before the first frame update
    void Start()
    {
        mainHand = items[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !mainHand.equipped)
        {
            mainHand.Equip(this.gameObject);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            mainHand.Unequip(this.gameObject);
        }
    }
}
