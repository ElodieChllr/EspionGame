using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarteAccesSlot : InventorySlot
{
    public override void Utiliser()
    {
        base.Utiliser();
        Debug.Log("Utilisation de la carte d'acc�s");
        // Impl�mentez la logique sp�cifique pour la carte d'acc�s ici
    }
}
