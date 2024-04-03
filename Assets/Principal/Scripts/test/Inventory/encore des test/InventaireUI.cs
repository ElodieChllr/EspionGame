using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventaireUI : MonoBehaviour
{
    public Image[] slots; // Tableau de GameObjects représentant les emplacements de l'inventaire
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
            // Parcourir chaque emplacement de l'inventaire
            for (int i = 0; i < inventory.items.Count; i++)
            {
                // Assurer que vous ne dépassez pas le nombre de slots disponibles
                if (i < slots.Length)
                {
                    // Obtenez l'objet de l'inventaire que vous souhaitez afficher dans cet emplacement
                    InventoryItem item = inventory.items[i];

                    // Assurez-vous que l'emplacement est un GameObject de type Image
                    Image slotImage = slots[i].GetComponent<Image>();

                    // Changez le sprite du slot pour représenter l'objet de l'inventaire
                    if (item != null && item.itemSprite != null && slotImage != null)
                    {
                        slotImage.sprite = item.itemSprite; // Utilisez le sprite de l'objet
                        slotImage.enabled = true; // Rendre le slot visible
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
