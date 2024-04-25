using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pnl_Pause;
    private bool isPaused;

    public PlayerInput playerInputRef;
    public GameObject player;

    //[SerializeField]
    //private GameObject itemsButton;
    [SerializeField]
    private GameObject bt_Pause;

    void Start()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (playerInputRef.actions["Pause"].WasReleasedThisFrame())
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DesactivateMenu();
        }
    }

    void ActivateMenu()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(bt_Pause, new BaseEventData(eventSystem));


        Time.timeScale = 0f;
        pnl_Pause.SetActive(true);


    }

    public void DesactivateMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pnl_Pause.SetActive(false);
    }


}
