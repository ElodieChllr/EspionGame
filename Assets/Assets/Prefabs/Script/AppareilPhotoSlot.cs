using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppareilPhotoSlot : InventorySlot
{
    public bool AppareilPhotoUtiliser = false;

    //public string CleDescription;
    public override void Utiliser()
    {
        base.Utiliser();
        Debug.Log("Utilisation de l'appareil photo");
        

        //StartCoroutine(UseCamera());
        EventsObjet.AppareilPhotoUsedEvent.Invoke();
    }

    private void Update()
    {
        if (openUse == false)
        {
            AppareilPhotoUtiliser = false;
        }
    }
    public override bool IsOpenUse()
    {
        return openUse;
    }

    //IEnumerator UseCamera()
    //{
    //    AppareilPhotoUtiliser = true;
    //    yield return new WaitForSeconds(0.5f);
    //    AppareilPhotoUtiliser = false;
    //}
}
