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
    public UnityEvent<GameObject> onSlotSelected;


    private InventaireController inventoryController;

    public bool openUse;

    private void Start()
    {
        inventoryController = FindObjectOfType<InventaireController>();

        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(SelectSlot);
        }
    }

    private void Update()
    {
        GameObject parentObject = GameObject.Find("Pnl_Inventory");

        if (parentObject != null)
        {
            Transform childTransform = parentObject.transform.Find("Pnl_Use");
            if (childTransform != null)
            {
                usePanel = childTransform.gameObject;
            }
        }
        else
        {
            usePanel = null;
        }
    }

    public void SelectSlot()
    {
        inventoryController._OnSlotSelected(gameObject);
        openUse = true;
        EventSystem.current.SetSelectedGameObject(null);
        inventoryController.SetLastSelectedSlot(this);
    }

    public virtual void Utiliser()
    {
        Debug.Log("Action générique de l'objet");
    }

    //public void OpenUse(GameObject selectedSlot)
    //{
    //    openUse = true;
    //    EventSystem.current.SetSelectedGameObject(null);
    //    inventoryController.SetLastSelectedSlot(this);
    //}
}






