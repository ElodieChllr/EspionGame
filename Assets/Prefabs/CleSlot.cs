using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleSlot : InventorySlot
{
    public override void Utiliser()
    {
        base.Utiliser();
        Debug.Log("Utilisation de la clé");
    }
}
