using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCle : MonoBehaviour
{
    public GameObject txt_objectif2;
    public GameObject txt_objectif3;

    public Animator endDoor_Anim;
    public AudioSource doorSound;

    public bool OnTrigger = false;



    public void OnKeyUsed()
    {
        doorSound.Play();
        endDoor_Anim.SetTrigger("OpenDoor");
        txt_objectif2.SetActive(false);
        txt_objectif3.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTrigger = false;
        }
    }
}
