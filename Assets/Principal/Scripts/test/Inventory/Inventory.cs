using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public List<Slots> inventorySlots = new List<Slots>();
    public Image crosshaire;
    public TMP_Text itemHoverText;


    public float raycastDistance = 5f;
    public LayerMask itemLayer;

    public PlayerInput playerControls;
    public GameObject player;

    public GameObject pnl_Inventaire;

    void Start()
    {
        playerControls = player.GetComponent<PlayerInput>();
        pnl_Inventaire.SetActive(false);


        foreach (Slots uiSlot in inventorySlots)
        {
            uiSlot.inistialiseSlot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        itemRaycast(Input.GetMouseButtonDown(0));

        if (playerControls.actions["Inventaire"].IsPressed())
        {
            pnl_Inventaire.SetActive(true);
        }

        if(pnl_Inventaire && playerControls.actions["Inventaire"].IsPressed())
        {
            pnl_Inventaire.SetActive(false);
        }
    }

    private void itemRaycast(bool hasClicked = false)
    {
        itemHoverText.text = "";
        Ray ray = Camera.main.ScreenPointToRay(crosshaire.transform.position);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, raycastDistance, itemLayer))
        {
            if(hit.collider != null)
            {
                if (hasClicked)
                {
                    Item newItem = hit.collider.GetComponent<Item>();
                    if (newItem)
                    {
                        addItemToInventory(newItem);
                    }
                }
                else
                {
                    Item newItem = hit.collider.GetComponent<Item>();
                    if (newItem)
                    {
                        itemHoverText.text = newItem.name;
                    }
                }
            }
        }
    }

    private void addItemToInventory(Item itemToAdd)
    {
        int leftOverQuantity = itemToAdd.currentQuantity;
        Slots openSlot = null;
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            Item heldItem = inventorySlots[i].getItem();

            if(heldItem != null && itemToAdd.name == heldItem.name)
            {
                int freeSpaceInSlot = heldItem.maxQuantity - heldItem.currentQuantity;

                if(freeSpaceInSlot >= leftOverQuantity)
                {
                    heldItem.currentQuantity += leftOverQuantity;
                    Destroy(itemToAdd.gameObject);
                    return;
                }
                else
                {
                    heldItem.currentQuantity = heldItem.maxQuantity;
                    leftOverQuantity -= freeSpaceInSlot;
                }
            }
            else if(heldItem == null)
            {
                if (openSlot)
                {
                    openSlot = inventorySlots[i];
                }                
            }
        }

        if(leftOverQuantity > 0 && openSlot)
        {
            openSlot.setItem(itemToAdd);
            itemToAdd.currentQuantity = leftOverQuantity;
            itemToAdd.gameObject.SetActive(false);
        }
        else
        {
            itemToAdd.currentQuantity = leftOverQuantity;
        }
    }


    //private void toggleInventory(bool enable)
    //{
    //    inventory.SetActive(enable);

    //    Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
    //    Cursor.visible = enable;

    //    Camera.main.GetComponent<>
    //}
}
