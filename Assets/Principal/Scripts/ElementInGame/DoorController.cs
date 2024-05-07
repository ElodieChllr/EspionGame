using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class DoorController : MonoBehaviour
{
    public GameObject txt_objectif;
    public GameObject txt_objectif2;
    public bool OnTrigger = false;
    public Animator doorAnimator;
    public AudioSource doorSound;

    public void OnCarteUsed()
    {
        doorSound.Play();
        doorAnimator.SetTrigger("OpenDoor");
        txt_objectif.SetActive(false);
        txt_objectif2.SetActive(true);
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
