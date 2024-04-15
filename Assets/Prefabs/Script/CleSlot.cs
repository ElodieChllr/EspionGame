using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleSlot : InventorySlot
{
    public bool CleUtiliser = false;
    public DoorCle doorCleRef;
    public override void Utiliser()
    {
        base.Utiliser();
        Debug.Log("Utilisation de la clé");
        CleUtiliser = true;

        if(CleUtiliser == true && doorCleRef.OnTrigger == true)
        {
            doorCleRef.OnKeyUsed();
        }
    }

    private void Update()
    {
        doorCleRef = FindObjectOfType<DoorCle>();
        if (openUse == false)
        {
            CleUtiliser = false;
        }


    }

    public override bool IsOpenUse()
    {
        return openUse;
    }
}
