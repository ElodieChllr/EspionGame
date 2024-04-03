using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;
    public GameObject itemObject; 
    public Sprite itemSprite;

    // Constructeur
    public InventoryItem(string name, int qty, GameObject obj, Sprite sprite)
    {
        itemName = name;
        quantity = qty;
        itemObject = obj;
        itemSprite = sprite;
    }
}
