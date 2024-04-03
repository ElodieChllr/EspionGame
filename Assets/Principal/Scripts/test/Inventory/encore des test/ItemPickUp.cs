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
                // Cr�er un nouvel objet d'inventaire en passant le sprite associ�
                InventoryItem newItem = new InventoryItem(itemName, quantity, gameObject, itemSprite);
                inventory.AddItem(newItem);

                //// Mettre � jour l'interface utilisateur de l'inventaire
                //if (inventoryUI != null)
                //    inventoryUI.UpdateUI();
                //Debug.Log("UpdateUI");

                // D�sactiver l'objet dans le monde apr�s l'avoir ramass�
                gameObject.SetActive(false);
            }
        }
    }
}

