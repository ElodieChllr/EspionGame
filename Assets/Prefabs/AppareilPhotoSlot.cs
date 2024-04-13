using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppareilPhotoSlot : InventorySlot
{
    public override void Utiliser()
    {
        base.Utiliser();
        Debug.Log("Utilisation de l'appareil photo");
        // Implémentez la logique spécifique pour l'appareil photo ici
    }
}
