using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventaireUI : MonoBehaviour
{
    public Image[] slots; 
    private InventaireController inventory;

    void Start()
    {
        inventory = GetComponent<InventaireController>();
        //UpdateUI();
    }

    public void UpdateUI()
    {
        Debug.Log("Updating UI...");

        // Vérifier si l'inventaire est disponible
        if (inventory != null && inventory.items != null)
        {
                Debug.Log("Inventory non null");
           
            for (int i = 0; i < inventory.items.Count; i++)
            {
               
                if (i < slots.Length)
                {
                   
                    InventoryItem item = inventory.items[i];

                    
                    Image slotImage = slots[i].GetComponent<Image>();

                    
                    if (item != null && item.itemSprite != null && slotImage != null)
                    {
                        slotImage.sprite = item.itemSprite; 
                        slotImage.enabled = true;
                    }
                    else
                    {
                        Debug.LogWarning("Item, itemSprite, or slotImage is null.");
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Inventory or inventory.items is null.");
        }


    }
}
