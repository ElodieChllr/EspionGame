using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pnl_Pause;
    private bool isPaused;

    public PlayerInput playerInputRef;
    public GameObject player;
    void Start()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
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
        Time.timeScale = 0f;
        pnl_Pause.SetActive(true);

    }

    public void DesactivateMenu()
    {
        Time.timeScale = 1f;
        pnl_Pause.SetActive(false);
    }


}
