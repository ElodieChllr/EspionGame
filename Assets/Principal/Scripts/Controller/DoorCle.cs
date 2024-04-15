using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCle : MonoBehaviour
{
    public GameObject txt_objectif2;
    public GameObject txt_objectif3;

    private void OnEnable()
    {
        EventsObjet.KeyUsedEvent.AddListener(OnKeyUsed);
    }

    private void OnDisable()
    {
        EventsObjet.KeyUsedEvent.RemoveListener(OnKeyUsed);
    }

    private void OnKeyUsed()
    {
        gameObject.SetActive(false);
        txt_objectif2.SetActive(false);
        txt_objectif3.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CleSlot cleSlot = other.GetComponent<CleSlot>();
            Debug.Log("in");
            if (cleSlot != null && cleSlot.CleUtiliser)
            {
                gameObject.SetActive(false);
            }


        }
    }
}
