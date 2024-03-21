using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovered;
    private Item heldItem;

    private Color opaque = new Color(1, 1, 1, 1);
    private Color transparent = new Color(1,1,1,0);

    private Image thisSlotImage;
    
    public void inistialiseSlot()
    {
        thisSlotImage = gameObject.GetComponent<Image>();
        thisSlotImage.sprite = null;
        thisSlotImage.color = transparent;
        setItem(null);
    }

    public void setItem(Item item)
    {
        heldItem = item;
        if(item != null)
        {
            thisSlotImage.sprite = heldItem.icon;
            thisSlotImage.color = opaque;
        }
        else
        {
            thisSlotImage.sprite = null;
            thisSlotImage.color = transparent;
        }
    }

    public Item getItem()
    {
        return heldItem;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        hovered = false;
    }

}
