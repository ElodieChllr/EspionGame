using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleSlot : InventorySlot
{
    public bool CleUtiliser = false;
    public override void Utiliser()
    {
        base.Utiliser();
        Debug.Log("Utilisation de la clé");
        CleUtiliser = true;

        EventsObjet.KeyUsedEvent.Invoke();
    }

    private void Update()
    {
        if(openUse == false)
        {
            CleUtiliser = false;
        }
    }

    public override bool IsOpenUse()
    {
        return openUse;
    }
}
