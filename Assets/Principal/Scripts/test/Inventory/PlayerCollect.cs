using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private GameObject visualInteractionCl�;
    [SerializeField] private GameObject visualInteractionCarte;
    [SerializeField] private GameObject visualInteractionAppareil;
    [SerializeField] private GameObject visualInteractionMalette;

    [Header("Cl�")]
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

    private void Start()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
    }
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Cl�")
        {
            //visualInteractionCl�.SetActive(true);
            obj.gameObject.SetActive(false);
            //Destroy(obj);
            lastButtonInstantiated = Instantiate(ClePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
            //if (playerInputRef.actions["Interagir"].IsPressed())
            //{
            //    Debug.Log("Cl� pris");
                
            //}            
        }

        if (obj.CompareTag("CarteAcces"))
        {
            visualInteractionCarte.SetActive(true);

            //if (playerInputRef.actions["Interagir"].WasReleasedThisFrame())
            //{
            //    gameObject.SetActive(false);
            //    lastButtonInstantiated = Instantiate(carteBombePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
            //}
            //visualInteractionCl�.SetActive(true);
            obj.gameObject.SetActive(false);
            //Destroy(obj);
            lastButtonInstantiated = Instantiate(carteBombePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
        }

        if (obj.tag == "AppareilPhoto")
        {
            //visualInteractionAppareil.SetActive(true);
            lastButtonInstantiated = Instantiate(AppareilPhotoPrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
            obj.gameObject.SetActive(false);
            //Destroy(obj);
            //if (playerInputRef.actions["Interagir"].IsPressed())
            //{
            //    Debug.Log("Cl� pris");

            //}            
        }

        if(obj.tag == "Malette")
        {
            visualInteractionMalette.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag == "Cl�")
        {
            visualInteractionCl�.SetActive(false);
        }

        if(obj.CompareTag("CarteAcces"))
        {
            visualInteractionCarte.SetActive(false);
        }

        if (obj.CompareTag("AppareilPhoto"))
        {
            visualInteractionAppareil.SetActive(false);
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
}
