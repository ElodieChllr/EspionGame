using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager Instance { get; private set; }
    [field: SerializeField] public ItemDataBase ItemDataBase { get; private set; }

    public void Awake()
    {
        ////if(Instance == null) ces 2 lignes est egale à celle d'en dessous genre c'est la meme chose
        ////Instance = this;
        //Instance ??= this;
        //for (int i = 0; i < ItemDataBase.itemDatas.Count; i++)
        //{
        //    ItemDataBase.itemDatas[i].InitId(i);
        //}
        //for (int i = 0; i < QuestDataBase.qDataList.Count; i++)
        //{
        //    QuestDataBase.qDataList[i].InitId(i);
        //}
    }

    public List<ItemDataBase> GetAllItemDataBases()
    {
        return new()
        {
            ItemDataBase,
        };
    }
}
