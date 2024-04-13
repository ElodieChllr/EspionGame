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
   
    public void OpenUse()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(useButton, new BaseEventData(eventSystem));
        usePanel.SetActive(true);
    }

    public void Cancel()
    {
        var eventSystem = EventSystem.current;
        //eventSystem.SetSelectedGameObject(Slot, new BaseEventData(eventSystem));
        usePanel.SetActive(false);
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
