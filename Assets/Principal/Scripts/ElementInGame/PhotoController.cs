using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhotoController : MonoBehaviour
{
    
    
    public DoorController DoorController;
    public bool IsTakenPhoto;
    public PlayerInput playerInputRef;
    public GameObject player;
    public GameObject Flash;
    public bool isPhoto; 
    private bool bruh;

    public bool tutoFini = false;
    public GameObject colliderTuto;

    public bool OnTrigger = false;
    public AppareilPhotoSlot appareilPhotoSlotRef;

    public AudioSource cameraSound;
    public GameObject camPng;

    public float vibrationIntensity = 0.5f;
    public float vibrationDuration = 0.5f;

    private Gamepad gamepad;

    [Header("Ref")]
    public PlayerCollect playerCollectRef;
    public PlayerController PlayerController;
   

    // Start is called before the first frame update
    void Start()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
        isPhoto = false;
        IsTakenPhoto = false;
        bruh=false;

        gamepad = Gamepad.current;

    }

    // Update is called once per frame
    void Update()
    {
        if(bruh == true)
        {
            gameObject.SetActive(false);
        }

       if(tutoFini == true)
        {
            colliderTuto.SetActive(false);
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            appareilPhotoSlotRef = FindAnyObjectByType<AppareilPhotoSlot>();
            camPng.SetActive(true);
            OnTrigger = true;
           
        }


     
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && playerInputRef.actions["Photo"].IsPressed())
        {           
           
            if (playerCollectRef.recupCam == true)
            {
                isPhoto = true;
                StartCoroutine(Flashbang());
                Debug.Log("photo taken");
            }
            
            
            //gameObject.SetActive(false);
        }
        else
        {
            isPhoto=false;
        }
        
    }
    public void OnTriggerExit(Collider other)
    {
        OnTrigger = false;
     
        camPng.SetActive(false);
    }
   
    private IEnumerator Flashbang ()
    {
        tutoFini = true;
        PlayerController.playerAnimator.SetTrigger("Photo");

        gamepad.SetMotorSpeeds(vibrationIntensity, vibrationIntensity);
        Invoke("StopVibration", vibrationDuration);
        cameraSound.Play();

        yield return new WaitForSeconds(0.8f);
        IsTakenPhoto = true;
        Flash.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        Debug.Log("ok");


        Flash.SetActive(false);
        bruh = true;
               
    }

    void StopVibration()
    {
        gamepad.SetMotorSpeeds(0f, 0f);
    }
}
