using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{

    [Header("Clé")]
    public GameObject ClePrefab;

    [Header("Appareil Photo")]
    public GameObject AppareilPhotoPrefab;

    [Header("Carte d'accees")]
    public GameObject carteBombePrefab;

    [Header("Inventaire")]
    public GameObject inventoryContent;
    private GameObject lastButtonInstantiated; 

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Clé")
        {
            Destroy(obj);
            lastButtonInstantiated = Instantiate(ClePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
        }

        if (obj.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            lastButtonInstantiated = Instantiate(carteBombePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
        }
    }

    public GameObject GetLastButtonInstantiated()
    {
        return lastButtonInstantiated;
    }
}
