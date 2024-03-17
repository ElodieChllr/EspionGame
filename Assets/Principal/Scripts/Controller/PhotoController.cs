using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhotoController : MonoBehaviour
{
    
    
    public DoorController DoorController;
    public PlayerController playerController;

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
       if(other.CompareTag("Player") && playerController.isJumpPressed == true)
        {
            DoorController.Phototaken++;
         
            Debug.Log(DoorController.Phototaken);
            gameObject.SetActive(false);

        }
    }
   
}
