using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SurveillanceZone : MonoBehaviour
{
    public PlayerController playerControllerRef;


    public GameObject pnl_GameOver;

    public GameObject bt_Retry;

    public bool gamePaused = false;

    public bool spoted = false;

    public Animator pnl_transitionAnim;
    void Start()
    {
        pnl_GameOver.SetActive(false);
    }


    void Update()
    {
        if (gamePaused == true)
        {
            Time.timeScale = 0f;
            Debug.Log("Pause");
            //ça ne marche pas , je sais pas pourquoi
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerControllerRef.globalSpeed > 3)
            {
                spoted = true;
                StartCoroutine(ToMuchNoise());
            }
        }
    }

    IEnumerator ToMuchNoise()
    {
        if (spoted == true)
        {
            Debug.Log("Trop vite");
            gamePaused = true;
            
            yield return new WaitForSeconds(0.5f);
            playerControllerRef.moveSpeed = 0;
            playerControllerRef.rotationSpeed = 0;
            pnl_transitionAnim.SetTrigger("GameOver");
            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene(2);
            //pnl_GameOver.SetActive(true);
            //var eventSystem = EventSystem.current;
            //eventSystem.SetSelectedGameObject(bt_Retry, new BaseEventData(eventSystem));
        }        
    }
}
