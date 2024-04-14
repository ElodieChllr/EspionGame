using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarteAccesSlot : InventorySlot
{
    public bool CarteUtiliser = false;
    public override void Utiliser()
    {
        base.Utiliser();
        Debug.Log("Utilisation de la carte d'accès");
        CarteUtiliser = true;

        EventsObjet.CarteAccesUsedEvent.Invoke();
    }

    private void Update()
    {
        if (openUse == false)
        {
            CarteUtiliser = false;
        }
    }
    public override bool IsOpenUse()
    {
        return openUse;
    }
}
