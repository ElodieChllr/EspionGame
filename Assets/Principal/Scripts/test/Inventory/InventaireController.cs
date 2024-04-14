using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventaireController : MonoBehaviour
{
    //public static InventaireController Instance { get; private set; }
    //public static bool IsOpening { get; private set; }
    //public SlotController CurrentSlotSelected { get; set; }

    //[SerializeField] private GameObject parentSlot;
    //[SerializeField] private GameObject prefabSlot;
    //[SerializeField] private Text txtInfo;

    //[SerializeField] private int nbSlot;

    //public List<SlotController> Slots { get; private set; }

    //public PlayerController playerInventaire { get; private set; }

    ////GIVING
    //public enum GIVING
    //{
    //    SEAMON,
    //    TEMPLE
    //}
    //public GIVING giving;
    //public GameObject bttnGive;

    //public void Awake()
    //{

    //    //Init();
    //    //OnSetOpen(false);
    //    //OnClose();
    //}

    //public void Init()
    //{
    //    Slots = new();
    //    for (int i = 0; i < nbSlot; i++)
    //    {
    //        SlotController newSlot = Instantiate(prefabSlot, parentSlot.transform).GetComponent<SlotController>();
    //        Slots.Add(newSlot);
    //    }
    //}

    //public void AddItem(ItemData item)
    //{
    //    if (Slots.Exists(x => x.ItemData == item && x.ItemData.value == item.value))
    //        Slots.Find(x => x.ItemData == item).OnAdd(item);
    //    else
    //        Slots.Find(x => x.ItemData == default).OnAdd(item);
    //}

    //public void OnSetOpen(bool value)
    //{
    //    Instance = value ? this : null; //quand l'inventaire est ouvert, on fait référence à celui là uniquement
    //    IsOpening = value;
    //    GameManager.GameActive = !value;
    //    //GameManager.GameActive
    //    playerInventaire = value ? FindObjectOfType<PlayerController>() : null;
    //    gameObject.SetActive(value);
    //    bttnGive.SetActive(false);
    //}

    //public void OnUse()
    //{
    //    CurrentSlotSelected.OnUse();
    //}

    //public void OnDrop(/*SlotController slot*/)
    //{
    //    //if (slot == null)
    //    //    slot = CurrentSlotSelected;
    //    CurrentSlotSelected.OnRemove();
    //}

    //public void RefreshUI(string value = "")
    //{
    //    txtInfo.text = value;
    //    //txtInfo.text = CurrentSlotSelected ? CurrentSlotSelected.ItemData.caption : "";
    //}

    ////Lorsqu'on ouvre l'inventaire, ouvre une version différente
    //public void GiveItemSetup(GameObject giving)
    //{
    //    if (giving.CompareTag("Seamon"))
    //    {
    //        this.giving = GIVING.SEAMON;
    //    }
    //    else if (giving.CompareTag(""))
    //    {
    //        this.giving = GIVING.TEMPLE;
    //    }
    //    bttnGive.SetActive(true);
    //}

    //public void OnGiveItem()
    //{
    //    switch (giving)
    //    {
    //        case GIVING.SEAMON:
    //            CurrentSlotSelected.OnFeed(playerInventaire);
    //            break;
    //        case GIVING.TEMPLE:
    //            break;
    //    }
    //}
    //public List<InventoryItem> items = new List<InventoryItem>();
    //public InventaireUI inventaireUiRef;


    //public void AddItem(InventoryItem item)
    //{
    //    items.Add(item);
    //    inventaireUiRef.UpdateUI();

    //}


    //public void RemoveItem(InventoryItem item)
    //{
    //    items.Remove(item);
    //    inventaireUiRef.UpdateUI();

    //}


    //public bool HasItem(string itemName)
    //{
    //    foreach (InventoryItem item in items)
    //    {
    //        if (item.itemName == itemName)
    //            return true;
    //    }
    //    return false;
    //}


    //public InventoryItem GetItem(string itemName)
    //{
    //    foreach (InventoryItem item in items)
    //    {
    //        if (item.itemName == itemName)
    //            return item;
    //    }
    //    return null;
    //}
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

    private InventorySlot lastSelectedSlot;
    private void Awake()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
        controls = new PlayerMap();

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
        if (playerInputRef.actions["Inventaire"].WasReleasedThisFrame())
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
        Time.timeScale = 0;
        inventoryPanel.SetActive(true);
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
        Time.timeScale = 1;
        inventoryPanel.SetActive(false);
        isInventoryOpen = false;
    }

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
    public void _OnSlotSelected(GameObject slotObject)
    {
        
        Debug.Log("Slot sélectionné : " + slotObject.name);
    }

    public void SetLastSelectedSlot(InventorySlot slot)
    {
        lastSelectedSlot = slot;
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

