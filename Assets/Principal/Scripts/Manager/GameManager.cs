using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject pnl_PauseGlobal;
    public GameObject pnl_Pause;

    public PlayerInput playerInputRef;
    public GameObject player;
    [SerializeField] private bool isPaused;

    private void Awake()
    {
        playerInputRef = player.GetComponent<PlayerInput>();
    }
    void Start()
    {
        
    }    
    private void Update()
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
        pnl_PauseGlobal.SetActive(true);
    }

    public void DesactivateMenu()
    {
        Time.timeScale = 1f;
        pnl_PauseGlobal.SetActive(false);
        isPaused = false;
    }
}
