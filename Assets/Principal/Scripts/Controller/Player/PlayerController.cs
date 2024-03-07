using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
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
    private Animator animator;




    PlayerMap playerMap;

    public PlayerInput playerInput;

    

    void Start()
    {
        animator = GetComponent<Animator>();    
        rb_player = GetComponent<Rigidbody>();
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
    // Update is called once per frame
    void Update()
    {
        tiltValue = controls.Player.Movement.ReadValue<Vector3>();
        float tiltX = tiltValue.x;
        float tiltY = tiltValue.y;

        // Utiliser tiltX et tiltY pour contrôler votre jeu en fonction de l'inclinaison du joystick
        Debug.Log("Tilt X: " + tiltX + ", Tilt Y: " + tiltY);
        AnimManager(tiltX, tiltY);
    }


    public void Move_and_Cam()
    {
        //Move
        Vector3 movement = playerMap.Player.Movement.ReadValue<Vector3>();
        rb_player.velocity = new Vector3(movement.x * moveSpeed * Time.fixedDeltaTime, rb_player.velocity.y * Time.fixedDeltaTime, movement.z * moveSpeed * Time.fixedDeltaTime);



        //CAM
        movement.Normalize();

        
        movement = Quaternion.Euler(0f, cameraMainTransform.eulerAngles.y, 0f) * movement;


        rb_player.velocity = movement * moveSpeed; //c'est ici que ça fait bug le jump


        if (playerInput.actions["Sprint"].IsInProgress())
        {
            rb_player.velocity = movement * sprintSpeed;
        }


        if (movement != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }


        Vector3 desiredPosition = transform.position - offset;
        Vector3 smoothedPosition = Vector3.Lerp(cameraMainTransform.position, desiredPosition, Time.deltaTime * 5f);
        cameraMainTransform.position = smoothedPosition;


        Quaternion lookRotation = Quaternion.LookRotation(transform.position - cameraMainTransform.position, Vector3.up);
        cameraMainTransform.rotation = Quaternion.Slerp(cameraMainTransform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //private void RecupeObject()
    //{

    //}

    //public void Jump(InputAction.CallbackContext context)
    //{
    //    if (isGrounded == true && context.performed)
    //    {
    //        isJumping = true;
    //        isGrounded = false;
    //        Debug.Log("jump");

    //        if (isJumping == true)
    //        {
    //            rb_player.AddForce(new Vector3(0, jumpingPower, 0), ForceMode.Impulse);
             
    //        }
    //        if (rb_player.velocity.y < 0f)
    //        {
    //            rb_player.velocity += Vector3.down * (Physics.gravity.y * -15f) * Time.fixedDeltaTime;
    //        }

    //    }
       
    //}   


    private void OnEnable()
    {
        playerMap.Enable();
    }

    private void OnDisable()
    {
        playerMap.Disable();
    }



    public void AnimManager(float X, float Z)
    {
        Vector2 velocity = new Vector2(X,Z); // recup les joysticks
        float animationMove = velocity.magnitude;
        if(velocity.magnitude >=1f)
        {
            velocity= velocity.normalized;
        }
        animator.SetFloat("Player_Velocity", animationMove);
    }


}




