using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private GameObject visualInteractionClé;
    [SerializeField] private GameObject visualInteractionCarte;

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

    private void Start()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
    }
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Clé")
        {
            visualInteractionClé.SetActive(true);
            Destroy(obj);
            lastButtonInstantiated = Instantiate(ClePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
            //if (playerInputRef.actions["Interagir"].IsPressed())
            //{
            //    Debug.Log("Clé pris");
                
            //}            
        }

        if (obj.CompareTag("CarteAcces"))
        {
            visualInteractionCarte.SetActive(true);

            if (playerInputRef.actions["Interagir"].WasReleasedThisFrame())
            {
                gameObject.SetActive(false);
                lastButtonInstantiated = Instantiate(carteBombePrefab, Vector3.zero, Quaternion.identity, inventoryContent.transform);
            }            
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
    }

    public GameObject GetLastButtonInstantiated()
    {
        return lastButtonInstantiated;
    }
}
