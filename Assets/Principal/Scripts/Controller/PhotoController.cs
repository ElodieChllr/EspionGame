using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhotoController : MonoBehaviour
{
    
    
    public DoorController DoorController;
    public PlayerController playerController;
    public GameObject pressA;

    // Start is called before the first frame update
    void Start()
    {
      
       
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            pressA.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        pressA.SetActive(false);
    }

}
