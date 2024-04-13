using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarteAccesSlot : InventorySlot
{
    public override void Utiliser()
    {
        base.Utiliser();
        Debug.Log("Utilisation de la carte d'accès");
        // Implémentez la logique spécifique pour la carte d'accès ici
    }
}
