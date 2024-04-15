using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarteAccesSlot : InventorySlot
{
    public bool CarteUtiliser = false;
    public DoorController doorControllerRef;
    //public Animator DoorAnimator;


    
    public override void Utiliser()
    {
        base.Utiliser();
        Debug.Log("Utilisation de la carte d'accès");
        CarteUtiliser = true;

        //EventsObjet.CarteAccesUsedEvent.Invoke();

        if(CarteUtiliser == true && doorControllerRef.OnTrigger == true)
        {
            doorControllerRef.OnCarteUsed();
        }
    }

    private void Update()
    {
        doorControllerRef = FindObjectOfType<DoorController>();
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
