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


         

        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(SelectSlot);
        }
    }

    private void Update()
    {

    }

    public void SelectSlot()
    {
        if (pnl_Use != null)
        {
            pnl_Use.SetActive(true);
        }

        openUse = true;
        txt_Description = GameObject.Find("Txt_Description").GetComponent<Text>();
        inventoryController._OnSlotSelected(gameObject);
        inventoryController.SetLastSelectedSlot(this);

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
}






