using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualInteraction;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;


    //public GameObject IlluBoris;


    private bool playerInRange;


    public PlayerInput playerControls;
    public GameObject player;

    //public ObjetsQuete objetsQuete;





    private void Awake()
    {
        playerInRange = false;
        visualInteraction.SetActive(false);
        playerControls = player.GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && DialogueManager.GetInstance().playerControls.actions["Interagir"].IsPressed())
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
            visualInteraction.SetActive(true);
            Debug.Log("In");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
            visualInteraction.SetActive(false);
            Debug.Log("Out");
        }
    }
}
