using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item item;
    private SpriteRenderer sprite;

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        Debug.Log(item);
        this.item = item;
        Debug.Log(item.icon);
        sprite.sprite = item.icon;
    }

}
