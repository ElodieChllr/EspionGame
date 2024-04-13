using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarteBombe : MonoBehaviour
{
    //public GameObject door;
    //private bool hasKey = false;

    
    [Header("Inventaire")]
    public GameObject carteBombePrefab;
    public GameObject inventoryContent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //hasKey = true;
            gameObject.SetActive(false);
            Instantiate(carteBombePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
        }
    }
}
