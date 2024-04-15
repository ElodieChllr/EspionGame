using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class DoorController : MonoBehaviour
{
    //public int Phototaken;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    Phototaken = 0; 
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    OpenDoor();
    //}
    //public void OpenDoor()
    //{
    //    if (Phototaken >= 3)
    //    {
    //        gameObject.SetActive(false);
    //        Debug.Log("Door is open");
    //    }
    //}
    public GameObject txt_objectif;
    public GameObject txt_objectif2;

    public Animator doorAnimator;

    private void Start()
    {
        //doorAnimator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        EventsObjet.CarteAccesUsedEvent.RemoveListener(OnCarteUsed);
    }

    private void OnCarteUsed()
    {
        doorAnimator.SetTrigger("OpenDoor");
        txt_objectif.SetActive(false);
        txt_objectif2.SetActive(true);

    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("InTrigger");
            CarteAccesSlot carteSlot = other.GetComponent<CarteAccesSlot>();
            if (carteSlot != null && carteSlot.CarteUtiliser)
            {
                Debug.Log("UtiliserCarte");
                EventsObjet.CarteAccesUsedEvent.AddListener(OnCarteUsed);
                //gameObject.SetActive(false);
            }
        }
    }

    
}
