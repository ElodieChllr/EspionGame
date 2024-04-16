using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera TpsCam/*, FpsCam*/; 
    public Rigidbody rb_player;
    public Transform cameraPivot;

    [Header("Variables")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpingPower;
    public float rotationSpeed = 20f;

    private Transform cameraMainTransform;

    private Vector3 offset;
    private PlayerMap controls;
    private Vector2 tiltValue;
    public Animator animator;

    public bool isSwitchPressed;

    public GameObject Pnl_inventaire;
    public bool pnl_inventaireOpen;
    
    public PlayerCollect playerCollectRef;

    PlayerMap playerMap;

    public AnimationCurve accelerationCurve;

    public PlayerInput playerInput;

    [Header("Boutons")]
    public GameObject bt_Inventaire;
    public GameObject bt_SlotBackground;

    void Start()
    {
        animator = GetComponent<Animator>();    
        rb_player = GetComponent<Rigidbody>();
        isSwitchPressed = false;

    }
    private void Awake()
    {
        playerMap = new PlayerMap();
        cameraMainTransform = Camera.main.transform;
        controls = new PlayerMap();
        controls.Enable();
    }
    private void FixedUpdate()
    {
        Move_and_Cam();
    }

    void Update()
    {

    }


    public void Move_and_Cam()
    {
        Vector3 movement = playerMap.Player.Movement.ReadValue<Vector3>();
        float moveMagnitude = movement.magnitude;

        float speed = moveMagnitude * moveSpeed;


        animator.SetFloat("Player_Velocity", moveMagnitude);




        //CAM
        movement.Normalize();

        Vector3 cameraForward = cameraMainTransform.forward;
        cameraForward.y = 0f;
        Vector3 movementDirection = Quaternion.LookRotation(cameraForward) * movement.normalized;
        transform.Translate(movementDirection * speed * Time.fixedDeltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }


        Vector3 desiredPosition = transform.position - offset;
        Vector3 smoothedPosition = Vector3.Lerp(cameraMainTransform.position, desiredPosition, Time.deltaTime * 5f);
        cameraMainTransform.position = smoothedPosition;


        Quaternion lookRotation = Quaternion.LookRotation(transform.position - cameraMainTransform.position, Vector3.up);
        cameraMainTransform.rotation = Quaternion.Slerp(cameraMainTransform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //public void Switch(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        Debug.Log("switchperformed");
    //        if(CameraSwitch.IsActiveCam(TpsCam))
    //        {
    //            CameraSwitch.Switch(FpsCam);
    //            isSwitchPressed =true;

    //        }
    //        else if (CameraSwitch.IsActiveCam(FpsCam))
    //        {
    //            CameraSwitch.Switch(TpsCam);
    //        }

    //    }
        
    //}


    private void OnEnable()
    {
        playerMap.Enable();

        //CameraSwitch.Register(TpsCam);
        //CameraSwitch.Register(FpsCam);
        //CameraSwitch.Switch(TpsCam);
    }

    private void OnDisable()
    {
        playerMap.Disable();
        //CameraSwitch.Unregister(TpsCam);
        //CameraSwitch.Unregister(FpsCam);
    }

  

}




