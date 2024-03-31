using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public bool Selected { get; private set; }

    [SerializeField] private Image imgItem;
    [SerializeField] private Text txtQuantity;
    [SerializeField] private GameObject bttnItem;
    [SerializeField] private Color pointerHoversColor, pointerClickColor;
    [SerializeField] private Image imgSelector;
    public enum SLOTCATEGORY
    {
        INVENTORY,
        CRAFT
    }
    public SLOTCATEGORY slotCat;

   // [field: SerializeField] public RecipeData RecipeData { get; private set; }
    public ItemData ItemData { get; private set; }

    private int _quantity = 0;
    public int Quantity
    {
        get => _quantity;
        private set { _quantity = value; RefreshUI(); }
    }
    bool pointerOnMe;

    private void Awake()
    {
        RefreshUI();
    }

    private void Update()
    {
        if (pointerOnMe && Input.GetMouseButtonDown(0))
        {
            OnSelected();
        }
    }

    public void OnAdd(ItemData item)
    {
        Quantity++;

        if (Quantity > 1)
            return;

        bttnItem.SetActive(true);
        ItemData = item;
        imgItem.sprite = item.sprite;
        imgItem.color = item.color;
    }

    //public void CraftInit(RecipeData rd)
    //{
    //    RecipeData = rd;
    //    imgItem.sprite = rd.icon;
    //}

    public void OnRemove()
    {
        Quantity--;

        if (Quantity > 0)
            return;

        bttnItem.SetActive(false);
        ItemData = default;
        imgItem.sprite = null;
        imgItem.color = Color.clear;
    }

    public void OnUse()
    {
        //InventaireController.Instance.playerInventaire._lifeController.GetHeal(ItemData.value);
        OnRemove();
    }

    public void OnFeed(PlayerController player) //SEAMONCONTENEMENT
    {
        //player.questController.CheckQuest(ItemData);
        //StatController.Instance.UpdateContentement(5);
        OnRemove();
    }

    public void OnSelected()
    {
        EventSystem.current.SetSelectedGameObject(transform.GetChild(transform.childCount - 1).gameObject);

        switch (slotCat)
        {
            case SLOTCATEGORY.INVENTORY:
                foreach (SlotController sc in InventaireController.Instance.Slots)
                {
                    sc.imgSelector.color = Color.clear;
                    sc.Selected = false;
                }
                InventaireController.Instance.CurrentSlotSelected = this;
                if (ItemData != default)
                    InventaireController.Instance.RefreshUI(ItemData.description);

                break;
            case SLOTCATEGORY.CRAFT:
                //foreach (SlotController sc in CraftController.Instance.Slots)
                //{
                //    sc.imgSelector.color = Color.clear;
                //    sc.Selected = false;
                //}
                //CraftController.Instance.CurrentSlotSelected = this;
                //CraftController.Instance.RefreshUI(RecipeData);
                break;
        }

        Selected = true;
        imgSelector.color = pointerClickColor;
    }

    void RefreshUI()
    {
        txtQuantity.enabled = Quantity > 0; // s'il y a plus de 0 objets, on affiche la quantité
        txtQuantity.text = Quantity.ToString();// update la quantité de l'item qui est dans le slot
    }

    public void OnPointerEnter(PointerEventData eventData) //on détecte la présence de notre souris sur le slot
    {
        if (!Selected)
            imgSelector.color = pointerHoversColor;
        pointerOnMe = true;
        switch (slotCat)
        {
            case SLOTCATEGORY.INVENTORY:
                if (ItemData == default) // Si on est sur un slot vide, on arrête la procédure
                    return;
                InventaireController.Instance.RefreshUI(ItemData.description + "\n Soigne : " + ItemData.value); // si on est sur un slot avec un item, on affiche sa description
                break;
            case SLOTCATEGORY.CRAFT:
               // if (RecipeData == default) // Si on est sur un slot vide, on arrête la procédure
                    return;
                //CraftController.Instance.SlotHovered = this;
                //CraftController.Instance.RefreshUI(RecipeData);
                //break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Selected)
            imgSelector.color = Color.clear;
        pointerOnMe = false;
        switch (slotCat)
        {
            case SLOTCATEGORY.INVENTORY:
                if (ItemData == default) // Si on est sur un slot vide, on arrête la procédure
                    return;
                InventaireController.Instance.RefreshUI(ItemData.description); // si on est sur un slot avec un item, on affiche sa description
                break;
            case SLOTCATEGORY.CRAFT:
                //CraftController.Instance.SlotHovered = null;
                //if (CraftController.Instance.CurrentSlotSelected)
                //    CraftController.Instance.RefreshUI(CraftController.Instance.CurrentSlotSelected.RecipeData);
                break;
        }
    }
}

