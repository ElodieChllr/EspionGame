using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData : MonoBehaviour
{
    public string label;
    [field: SerializeField] public int ID { get; private set; }
    [TextArea]
    public string description;
    public int quantityMax = 99;
    public Sprite sprite;
    public int value;
    public Color color;
    public int quality;
    public enum TYPE
    {
        NORMAL,
        CURRY,
        ICE
    }
    TYPE foodType;

    public ItemData()
    { }
    public ItemData(string label, string description, int value)
    {
        this.label = label;
        this.description = description;
        this.value = value;
        this.quantityMax = 99;
    }
    public ItemData(string label, string description, Sprite sprite, int value)
    {
        this.label = label;
        this.description = description;
        this.sprite = sprite;
        this.value = value;
        this.quantityMax = 99;
    }

    public void InitId(int i)
    {
        if (ID == 0)
            ID = i;
    }
}
