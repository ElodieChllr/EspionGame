using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject useButton;
    public GameObject pnl_Use;
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

        inventoryController = FindObjectOfType<InventaireController>();

        Button _button = GetComponent<Button>();

        if (_button != null)
        {
            _button.onClick.AddListener(SelectSlot);
        }
    }

    private void Update()
    {
        //GameObject parentObject = GameObject.Find("Pnl_Inventory");

        //if (parentObject != null)
        //{
        //    Transform childTransform = parentObject.transform.Find("Pnl_Use");
        //    if (childTransform != null)
        //    {
        //        usePanel = childTransform.gameObject;
        //    }
        //}
        //else
        //{
        //    usePanel = null;
        //}
    }

    public void SelectSlot()
    {
        if (pnl_Use != null)
        {
            pnl_Use.SetActive(true);
        }

        inventoryController._OnSlotSelected(gameObject);
        EventSystem.current.SetSelectedGameObject(null);
        inventoryController.SetLastSelectedSlot(this);
        openUse = true;
    }

    public virtual void Utiliser()
    {
        Debug.Log("Action générique de l'objet");
    }

    public virtual bool IsOpenUse()
    {
        return openUse;
    }

    //public void OpenUse(GameObject selectedSlot)
    //{
    //    openUse = true;
    //    EventSystem.current.SetSelectedGameObject(null);
    //    inventoryController.SetLastSelectedSlot(this);
    //}
}






