using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFinal : MonoBehaviour
{
    
    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerIn");
        }
    }
}
