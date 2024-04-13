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
        // Abonnez-vous � l'�v�nement de s�lection de tous les slots de l'inventaire
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

        // D�sactiver le panel
        usePanel.SetActive(false);

        // Trouver tous les objets dans la sc�ne qui ont le script InventorySlot attach�
        InventorySlot[] scripts = FindObjectsOfType<InventorySlot>();

        // Parcourir tous les objets qui ont le script InventorySlot attach�
        foreach (InventorySlot script in scripts)
        {
            // D�finir le bool�en openUse sur false pour chaque objet
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
        Debug.Log("Objet utilis� : " + slotObject.name);
    }
}
