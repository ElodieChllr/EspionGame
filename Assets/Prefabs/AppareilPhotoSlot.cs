using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppareilPhotoSlot : InventorySlot
{
    public override void Utiliser()
    {
        base.Utiliser();
        Debug.Log("Utilisation de l'appareil photo");
        // Impl�mentez la logique sp�cifique pour l'appareil photo ici
    }
}
