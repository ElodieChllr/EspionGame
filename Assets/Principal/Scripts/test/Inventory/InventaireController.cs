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
    public bool isInventoryOpen;
    public PlayerCollect playerCollectRef;
    //public GameObject backgroundButton;
    public GameObject bt_Slot;

    public GameObject pnl_Use;
    //public InventorySlot inventorySlotRef;

    private PlayerInput playerInputRef;
    private PlayerMap controls;

    private void Awake()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
        controls = new PlayerMap();

        //controls.Player.Inventaire.performed += ctx => ToggleInventoryMenu();
        controls.Player.InventaireNavigation.Enable();

        controls.InventaireNavigation.Up.performed += ctx => InventoryNavigationUp();
        controls.InventaireNavigation.Down.performed += ctx => InventoryNavigationDown();
        controls.InventaireNavigation.Left.performed += ctx => InventoryNavigationLeft();
        controls.InventaireNavigation.Right.performed += ctx => InventoryNavigationRight();
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
        }

        if (isInventoryOpen)
        {
            OpenInventoryPanel();
            controls.Player.InventaireNavigation.Enable();
            controls.Player.Inventaire.Disable();
        }
        else
        {
            CloseInventoryPanel();
            controls.Player.InventaireNavigation.Disable();
            controls.Player.Inventaire.Enable();
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
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(lastButton, new BaseEventData(eventSystem));
        }
        else
        {
            
        }
    }

    private void CloseInventoryPanel()
    {
        Time.timeScale = 1;
        inventoryPanel.SetActive(false);
        isInventoryOpen = false;
    }

    private void InventoryNavigationUp()
    {

        GameObject currentButton = EventSystem.current.currentSelectedGameObject;


        Selectable currentSelectable = currentButton.GetComponent<Selectable>();
        Selectable nextSelectable = currentSelectable.FindSelectableOnUp();


        if (nextSelectable != null)
        {
            GameObject nextButton = nextSelectable.gameObject;
            EventSystem.current.SetSelectedGameObject(nextButton);
        }
    }

    private void InventoryNavigationDown()
    {

        GameObject currentButton = EventSystem.current.currentSelectedGameObject;


        Selectable currentSelectable = currentButton.GetComponent<Selectable>();
        Selectable nextSelectable = currentSelectable.FindSelectableOnDown();


        if (nextSelectable != null)
        {
            GameObject nextButton = nextSelectable.gameObject;
            EventSystem.current.SetSelectedGameObject(nextButton);
        }
    }

    private void InventoryNavigationLeft()
    {

        GameObject currentButton = EventSystem.current.currentSelectedGameObject;


        Selectable currentSelectable = currentButton.GetComponent<Selectable>();
        Selectable nextSelectable = currentSelectable.FindSelectableOnLeft();

        if (nextSelectable != null)
        {
            GameObject nextButton = nextSelectable.gameObject;
            EventSystem.current.SetSelectedGameObject(nextButton);
        }
    }

    private void InventoryNavigationRight()
    {

        GameObject currentButton = EventSystem.current.currentSelectedGameObject;

        Selectable currentSelectable = currentButton.GetComponent<Selectable>();
        Selectable nextSelectable = currentSelectable.FindSelectableOnRight();


        if (nextSelectable != null)
        {
            GameObject nextButton = nextSelectable.gameObject;
            EventSystem.current.SetSelectedGameObject(nextButton);
        }
    }
}

