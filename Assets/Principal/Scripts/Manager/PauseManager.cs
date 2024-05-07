using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused;

    public GameObject pnl_Pause;
    public GameObject pnl_Option;
    public GameObject pnl_Bindings;

    public PlayerInput playerInputRef;
    public GameObject player;

    [Header("Buton")]
    [SerializeField] private GameObject bt_Pause;
    [SerializeField] private GameObject bt_Option;
    [SerializeField] private GameObject Bt_Bindings;


    public Slider sensitivitySliderX;
    public Slider sensitivitySliderY;
    public CinemachineFreeLook freeLookCinemachine;
    

    void Start()
    {
        playerInputRef = player.GetComponent<PlayerInput>();


        sensitivitySliderX.value = 300f;
        sensitivitySliderY.value = 2f;


        sensitivitySliderX.onValueChanged.AddListener(OnSliderChangedX);
        sensitivitySliderY.onValueChanged.AddListener(OnSliderChangedY);
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
        pnl_Pause.SetActive(false);
        pnl_Option.SetActive(true);
    }

    public void DesactivateOption()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(bt_Pause, new BaseEventData(eventSystem));
        pnl_Pause.SetActive(true);
        pnl_Option.SetActive(false);
    }


    public void OpenBingings()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(Bt_Bindings, new BaseEventData(eventSystem));
        pnl_Option.SetActive(false);
        pnl_Bindings.SetActive(true);
    }

    public void CloseBindings()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(bt_Option, new BaseEventData(eventSystem));
        pnl_Option.SetActive(true);
        pnl_Bindings.SetActive(false);
    }

    private void OnSliderChangedX(float value)
    {
        freeLookCinemachine.m_XAxis.m_MaxSpeed = value;
        
    }

    private void OnSliderChangedY(float value)
    {
        freeLookCinemachine.m_YAxis.m_MaxSpeed = value;
    }


    
}
