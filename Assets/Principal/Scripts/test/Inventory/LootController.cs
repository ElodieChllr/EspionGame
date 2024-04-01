using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public int loot;
    SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Init(int loot)
    {
        this.loot = loot;
        sprite.sprite = DataBaseManager.Instance.ItemDataBase.itemDatas.Find(x => x.ID == loot).sprite;
    }

    public void Loot(InventaireController inventory)
    {
        inventory.AddItem(DataBaseManager.Instance.ItemDataBase.itemDatas.Find(x => x.ID == loot));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Loot(collision.gameObject.GetComponent<PlayerController>().inventaireController);
            Destroy(gameObject);
        }
    }
}
