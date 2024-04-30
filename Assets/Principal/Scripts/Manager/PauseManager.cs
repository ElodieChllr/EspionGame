using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pnl_Pause;
    public static bool isPaused;

    public GameObject pnl_Option;

    public PlayerInput playerInputRef;
    public GameObject player;

    [Header("Buton")]
    [SerializeField] private GameObject bt_Pause;
    [SerializeField] private GameObject bt_Option;

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
        //var eventSystem = EventSystem.current;
        //eventSystem.SetSelectedGameObject(bt_Pause, new BaseEventData(eventSystem));


        Time.timeScale = 0f;
        pnl_Pause.SetActive(true);


    }

    public void DesactivateMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pnl_Pause.SetActive(false);
    }

    public void ActivateOption()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(bt_Option, new BaseEventData(eventSystem));
        pnl_Option.SetActive(true);
        pnl_Pause.SetActive(false);
    }

    public void DesactivateOption()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(bt_Pause, new BaseEventData(eventSystem));
        pnl_Option.SetActive(false);
        pnl_Pause.SetActive(true);
    }
}
