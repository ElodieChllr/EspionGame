using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventaireManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject usePanel;

    public GameObject useButton;
    public GameObject Slot;
    private void Start()
    {
        // Abonnez-vous à l'événement de sélection de tous les slots de l'inventaire
        InventorySlot[] slots = FindObjectsOfType<InventorySlot>();
        foreach (InventorySlot slot in slots)
        {
            slot.onSlotSelected.AddListener(OnSlotSelected);
        }
    }

    private void Update()
    {
        
    }
    public void OpenUse()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(useButton, new BaseEventData(eventSystem));
        usePanel.SetActive(true);
    }

    public void Cancel()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(Slot, new BaseEventData(eventSystem));

        // Désactiver le panel
        usePanel.SetActive(false);

        // Trouver tous les objets dans la scène qui ont le script InventorySlot attaché
        InventorySlot[] scripts = FindObjectsOfType<InventorySlot>();

        // Parcourir tous les objets qui ont le script InventorySlot attaché
        foreach (InventorySlot script in scripts)
        {
            // Définir le booléen openUse sur false pour chaque objet
            script.openUse = false;
        }

    }
    private void OnSlotSelected(GameObject slotObject)
    {

        Debug.Log(slotObject.name);

        UseItem(slotObject);
    }

    private void UseItem(GameObject slotObject)
    { 
        Debug.Log("Objet utilisé : " + slotObject.name);
    }
}
