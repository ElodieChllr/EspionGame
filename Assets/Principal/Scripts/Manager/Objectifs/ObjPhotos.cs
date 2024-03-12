using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjPhotos : MonoBehaviour
{

    public PlayerInput playerInputRef;
    public GameObject player;


    private void Awake()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
    }
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
            Debug.Log("Player In");

            if (playerInputRef.actions["Photo"].WasPressedThisFrame())
            {
                Debug.Log("Photo prise");
            }


        }
        
    }
}
