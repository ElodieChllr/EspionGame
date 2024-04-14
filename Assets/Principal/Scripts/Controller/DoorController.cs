using System.Collections;
using System.Collections.Generic;
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
