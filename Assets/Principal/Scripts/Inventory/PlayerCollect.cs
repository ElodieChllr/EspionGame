using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private GameObject visualInteractionClé;
    [SerializeField] private GameObject visualInteractionCarte;
    //[SerializeField] private GameObject visualInteractionAppareil;
    [SerializeField] private GameObject visualInteractionMalette;

    [Header("Clé")]
    public GameObject ClePrefab;

    [Header("Appareil Photo")]
    public GameObject AppareilPhotoPrefab;

    [Header("Carte d'accees")]
    public GameObject carteBombePrefab;

    [Header("Inventaire")]
    public GameObject inventoryContent;
    private GameObject lastButtonInstantiated;


    public PlayerInput playerInputRef;
    public GameObject player;
    public Animator playerAnim;

    public GameObject camObj;
    public bool recupCam = false;

    private void Start()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
        camObj.SetActive(false);
    }
    private void OnTriggerEnter(Collider obj)
    {

        //if (obj.tag == "AppareilPhoto")
        //{
        //    playerAnim.SetTrigger("Recup");


        //    lastButtonInstantiated = Instantiate(AppareilPhotoPrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
        //    obj.gameObject.SetActive(false);            
        //}

        if(obj.tag == "Malette")
        {
            visualInteractionMalette.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag == "Clé")
        {
            visualInteractionClé.SetActive(false);
        }

        if(obj.CompareTag("CarteAcces"))
        {
            visualInteractionCarte.SetActive(false);
        }

        if (obj.CompareTag("AppareilPhoto"))
        {
            //visualInteractionAppareil.SetActive(false);
        }

        if (obj.tag == "Malette")
        {
            visualInteractionMalette.SetActive(false);
        }
    }

    public GameObject GetLastButtonInstantiated()
    {
        return lastButtonInstantiated;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CarteAcces"))
        {
            visualInteractionCarte.SetActive(true);

            if (playerInputRef.actions["Interagir"].WasReleasedThisFrame())
            {
                playerAnim.SetTrigger("Recup");
                lastButtonInstantiated = Instantiate(carteBombePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
                other.gameObject.SetActive(false);
            }
        }

        if (other.tag == "Clé")
        {
            visualInteractionClé.SetActive(true);

            if (playerInputRef.actions["Interagir"].WasReleasedThisFrame())
            {
                playerAnim.SetTrigger("Recup");
                lastButtonInstantiated = Instantiate(ClePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
                other.gameObject.SetActive(false);
            }        
        }

        if (other.tag == "AppareilPhoto")
        {
            //visualInteractionAppareil.SetActive(true);

            if (playerInputRef.actions["Interagir"].WasReleasedThisFrame())
            {
                playerAnim.SetTrigger("Recup");
                camObj.SetActive(true);
                recupCam = true;
                other.gameObject.SetActive(false);
            }
        }
    }

}