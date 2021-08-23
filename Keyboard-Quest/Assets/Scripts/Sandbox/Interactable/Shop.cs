using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;



[System.Serializable]
public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    public ItemListing[] itemsOnSale;
    public void ShowAffirmative()
    {
        shopUI.SetActive(true);
    }

    public void ShowNegative()
    {
        shopUI.SetActive(false);
    }
}
