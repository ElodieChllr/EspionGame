using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb_player;
    public Transform cameraPivot;

    private float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpingPower;
    private float rotationSpeed = 2f;


    private Transform cameraMainTransform;

    private Vector3 offset;

    private bool isJumping;
    private bool isGrounded;
  
    public LayerMask groundLayer;

    PlayerMap playerMap;

    public PlayerInput playerInput;

    void Start()
    {
        isGrounded = true;
        rb_player = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        playerMap = new PlayerMap();
        cameraMainTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move_and_Cam();
        Sprint();
        //RecupeObject();
        //RotateCamera();
    }
    //public void RotateCamera()
    //{
    //    Vector2 rotationInput = playerMap.Player.Camera.ReadValue<Vector2>();
    //    cameraPivot.Rotate(Vector3.up, rotationInput.x * rotationSpeed, Space.World);        
    //}

    public void Sprint()
    {
        if (playerInput.actions["Sprint"].IsInProgress())
        {
            Vector3 movement = playerMap.Player.Movement.ReadValue<Vector3>();
            rb_player.velocity = new Vector3(movement.x * sprintSpeed, movement.y * jumpingPower, movement.z * sprintSpeed);
            Debug.Log("Sprint");
        }
    }
    public void Move_and_Cam()
    {
        Vector3 movement = playerMap.Player.Movement.ReadValue<Vector3>();
        rb_player.velocity = new Vector3(movement.x * moveSpeed, rb_player.velocity.y, movement.z * moveSpeed);

        


        
        movement.Normalize();

        
        movement = Quaternion.Euler(0f, cameraMainTransform.eulerAngles.y, 0f) * movement;

        
        rb_player.velocity = movement * moveSpeed;

        
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

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded == true && context.performed)
        {
            isJumping = true;
            isGrounded = false;
            Debug.Log("jump");

            if (isJumping == true)
            {
                rb_player.AddForce(new Vector3(0, jumpingPower, 0), ForceMode.Impulse);
             
            }
            if (rb_player.velocity.y < 0f)
            {
                rb_player.velocity += Vector3.down * (Physics.gravity.y * -2f) * Time.fixedDeltaTime;
            }

        }
       
    }
     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
            isJumping = false;
            Debug.Log("isGrounded");
        }
      
    }

    


    private void OnEnable()
    {
        playerMap.Enable();
    }

    private void OnDisable()
    {
        playerMap.Disable();
    }






  
}


