using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemDataBase", menuName = "DataBase/Item", order = 1)]
public class ItemDataBase : ScriptableObject
{
    public List<ItemData> itemDatas = new();
    public void Init()
    {
        itemDatas.Add(new ItemData("Nom de l'item", "Super objet", 5));
    }
}
