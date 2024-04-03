using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;
    public GameObject itemObject; // Référence à l'objet 3D dans le monde
    public Sprite itemSprite; // Ajoutez cette variable pour stocker le sprite de l'objet

    // Constructeur
    public InventoryItem(string name, int qty, GameObject obj, Sprite sprite)
    {
        itemName = name;
        quantity = qty;
        itemObject = obj;
        itemSprite = sprite; // Affectez le sprite passé au constructeur
    }
}
