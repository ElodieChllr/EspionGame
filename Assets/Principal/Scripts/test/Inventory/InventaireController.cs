using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventaireController : MonoBehaviour
{    
    public GameObject player;
    public GameObject inventoryPanel;
    public GameObject pnl_Use;
    public GameObject usePanel;
    public GameObject bt_use;
    public GameObject bt_cancel;
    public PlayerCollect playerCollectRef;

    private bool isInventoryOpen;
    private PlayerInput playerInputRef;
    private PlayerMap controls;

    public Animator inventaire_Anim;

    private InventorySlot lastSelectedSlot;
    private void Awake()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
        controls = new PlayerMap();
        inventoryPanel.SetActive(false);

        controls.Player.InventaireNavigation.Enable();

        controls.InventaireNavigation.Up.performed += ctx => InventoryNavigationUp();
        controls.InventaireNavigation.Down.performed += ctx => InventoryNavigationDown();
        controls.InventaireNavigation.Left.performed += ctx => InventoryNavigationLeft();
        controls.InventaireNavigation.Right.performed += ctx => InventoryNavigationRight();

        InventorySlot[] slots = FindObjectsOfType<InventorySlot>();
        foreach (InventorySlot slot in slots)
        {
            slot.onSlotSelected.AddListener(OnSlotSelected);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        if (playerInputRef.actions["Inventaire"].WasReleasedThisFrame() && PauseManager.isPaused == false)
        {
            
            isInventoryOpen = !isInventoryOpen;
            if (isInventoryOpen)
            {
                OpenInventoryPanel();
            }
            else
            {
                CloseInventoryPanel();
            }
        }

        InventorySlot[] slots = FindObjectsOfType<InventorySlot>();
    InventorySlot lastSelectedSlot = null;
    foreach (InventorySlot slot in slots)
    {
        if (slot == null) continue;

        if (EventSystem.current.currentSelectedGameObject == slot.gameObject)
        {
            lastSelectedSlot = slot;
            break;
        }
    }

    
        if (lastSelectedSlot != null && lastSelectedSlot.IsOpenUse())
        {
            pnl_Use.SetActive(true);
            bt_use.GetComponent<Selectable>().interactable = true;
            bt_cancel.GetComponent<Selectable>().interactable = true;
            EventSystem.current.SetSelectedGameObject(bt_use);
        }
    }

    private void OpenInventoryPanel()
    {
        inventoryPanel.SetActive(true);
        inventaire_Anim.SetTrigger("OpenInventaire");
        isInventoryOpen = true;

        GameObject lastButton = playerCollectRef.GetLastButtonInstantiated();
        if (lastButton != null)
        {
            EventSystem.current.SetSelectedGameObject(lastButton);
        }
    }

    public void Cancel()
    {
        usePanel.SetActive(false);
        InventorySlot[] scripts = FindObjectsOfType<InventorySlot>();
        foreach (InventorySlot script in scripts)
        {
            script.openUse = false;
        }
    }

    private void OnSlotSelected(GameObject slotObject)
    {
        UseItem(slotObject);
    }

    private void UseItem(GameObject slotObject)
    {
        Debug.Log("Objet utilisé : " + slotObject.name);
    }

    private void CloseInventoryPanel()
    {
        //Time.timeScale = 1;
        inventaire_Anim.SetTrigger("CloseInventaire");
        inventoryPanel.SetActive(false);
        isInventoryOpen = false;
    }

    #region Navigation
    private void InventoryNavigationUp()
    {
        Selectable currentButton = EventSystem.current.currentSelectedGameObject?.GetComponent<Selectable>();
        if (currentButton != null)
        {
            Selectable nextSelectable = currentButton.FindSelectableOnUp();
            if (nextSelectable != null)
            {
                GameObject nextButton = nextSelectable.gameObject;
                EventSystem.current.SetSelectedGameObject(nextButton);
            }
        }
    }

    private void InventoryNavigationDown()
    {
        Selectable currentButton = EventSystem.current.currentSelectedGameObject?.GetComponent<Selectable>();
        if (currentButton != null)
        {
            Selectable nextSelectable = currentButton.FindSelectableOnDown();
            if (nextSelectable != null)
            {
                GameObject nextButton = nextSelectable.gameObject;
                EventSystem.current.SetSelectedGameObject(nextButton);
            }
        }
    }

    private void InventoryNavigationLeft()
    {
        Selectable currentButton = EventSystem.current.currentSelectedGameObject?.GetComponent<Selectable>();
        if (currentButton != null)
        {
            Selectable nextSelectable = currentButton.FindSelectableOnLeft();
            if (nextSelectable != null)
            {
                GameObject nextButton = nextSelectable.gameObject;
                EventSystem.current.SetSelectedGameObject(nextButton);
            }
        }
    }

    private void InventoryNavigationRight()
    {
        Selectable currentButton = EventSystem.current.currentSelectedGameObject?.GetComponent<Selectable>();
        if (currentButton != null)
        {
            Selectable nextSelectable = currentButton.FindSelectableOnRight();
            if (nextSelectable != null)
            {
                GameObject nextButton = nextSelectable.gameObject;
                EventSystem.current.SetSelectedGameObject(nextButton);
            }
        }
    }
    #endregion


    public void SetLastSelectedSlot(InventorySlot slot)
    {
        lastSelectedSlot = slot;
        Debug.Log("Slot sélectionné : " + slot.name);
    }

    public void Utiliser()
    {

        if (lastSelectedSlot != null)
        {
            lastSelectedSlot.Utiliser();
        }
        else
        {
            Debug.Log("Aucun slot sélectionné pour utiliser");
        }
    }
}

