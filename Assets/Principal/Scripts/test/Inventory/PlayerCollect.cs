using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{

    [Header("Cl�")]
    public GameObject ClePrefab;

    [Header("Appareil Photo")]
    public GameObject AppareilPhotoPrefab;

    [Header("Inventaire")]
    public GameObject inventoryContent;

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Cl�")
        {
            Destroy(obj);
            //obj.enabled = false;
            Instantiate(ClePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
        }

        //if (obj.tag == "Appareil Photo")
        //{
        //    Destroy(obj);
        //    Instantiate(ClePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
        //}
    }
}
