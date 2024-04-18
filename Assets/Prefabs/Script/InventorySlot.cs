using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject useButton;
    public GameObject pnl_Use;
    public UnityEvent<GameObject> onSlotSelected;

    public string description;

    public Text txt_Description; 

    private InventaireController inventoryController;

    public bool openUse;

    private void Start()
    {
        inventoryController = FindObjectOfType<InventaireController>();


        txt_Description = GameObject.Find("Txt_Description").GetComponent<Text>(); 

        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(SelectSlot);
        }

        //if (_button != null)
        //{
        //    _button.onClick.AddListener(SelectSlot);
        //}
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

        openUse = true;
        inventoryController._OnSlotSelected(gameObject);
        inventoryController.SetLastSelectedSlot(this);

        //txt_Description.text = description;
        Debug.Log(this.description);
        txt_Description.text = description;


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






