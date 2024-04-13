using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject useButton;
    public GameObject usePanel;
    public UnityEvent <GameObject> onSlotSelected;

    

    private void Start()
    {
        


        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(() => OpenUse(gameObject));
        }


    }
    private void Update()
    {
        GameObject parentObject = GameObject.Find("Pnl_Inventory");

        // Rechercher le GameObject enfant à partir du parent, même s'il est désactivé
        if (parentObject != null)
        {
            Transform childTransform = parentObject.transform.Find("Pnl_Use");
            if (childTransform != null)
            {
                usePanel = childTransform.gameObject;
            }
        }
        usePanel = GameObject.FindWithTag("pnl_Use");
    }

    public void SelectSlot()
    {
        onSlotSelected.Invoke(gameObject);
    }

    public void OpenUse(GameObject selectedSlot)
    {
        usePanel.SetActive(true);
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(useButton, new BaseEventData(eventSystem));
    }
}
