using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceZone : MonoBehaviour
{
    public PlayerController playerControllerRef;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerControllerRef.globalSpeed > 3)
            {
                Debug.Log("Trop vite");
            }
        }
    }
}
