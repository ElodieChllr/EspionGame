using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public string itemName;
    public int quantity = 1;
    public Sprite itemSprite;
    private InventaireUI inventoryUI;

    void Start()
    {
        inventoryUI = FindObjectOfType<InventaireUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventaireController inventory = other.GetComponent<InventaireController>();
            if (inventory != null)
            {
                
                InventoryItem newItem = new InventoryItem(itemName, quantity, gameObject, itemSprite);
                inventory.AddItem(newItem);

               
                gameObject.SetActive(false);
            }
        }
    }
}

