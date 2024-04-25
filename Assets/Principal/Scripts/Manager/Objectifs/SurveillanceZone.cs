using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SurveillanceZone : MonoBehaviour
{
    public PlayerController playerControllerRef;

    //public GameObject txt_ToMuchNoise;

    public GameObject pnl_GameOver;

    public GameObject bt_Retry;
    void Start()
    {
        pnl_GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerControllerRef.globalSpeed > 3)
            {
                Time.timeScale = 0f;
                StartCoroutine(ToMuchNoise());

            }
        }
    }

    IEnumerator ToMuchNoise()
    {
        //txt_ToMuchNoise.SetActive(true);

        Debug.Log("Trop vite");
        yield return new WaitForSeconds(1);
        

        pnl_GameOver.SetActive(true);
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(bt_Retry, new BaseEventData(eventSystem));
    }
}
