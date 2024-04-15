using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhotoController : MonoBehaviour
{
    
    
    public DoorController DoorController;
    public PlayerController playerController;
    public GameObject pressA;
    public bool IsTakenPhoto;
    public PlayerInput playerInputRef;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
        IsTakenPhoto = false;
        
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

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && playerInputRef.actions["Photo"].IsPressed())
        {
            IsTakenPhoto = true;
            Debug.Log("photo taken");
            gameObject.SetActive(false);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        pressA.SetActive(false);
    }
   
  
}
