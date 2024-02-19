using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb_player;
    public Transform cameraPivot;

    private float moveSpeed = 5f;
    private float sprintSpeed = 10f;
    public float jumpingPower;
    private float rotationSpeed = 2f;


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
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //RecupeObject();
        RotateCamera();

        // isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        if (isJumping == true)
        {
            rb_player.AddForce(new Vector3(0, jumpingPower, 0), ForceMode.Impulse);
        }

    }
    public void RotateCamera()
    {
        Vector2 rotationInput = playerMap.Player.Camera.ReadValue<Vector2>();
        cameraPivot.Rotate(Vector3.up, rotationInput.x * rotationSpeed, Space.World);
    }


    public void Move()
    {
        Vector3 movement = playerMap.Player.Movement.ReadValue<Vector3>();
        rb_player.velocity = new Vector3(movement.x * moveSpeed, 0, movement.z * moveSpeed);

        if (playerInput.actions["Sprint"].IsInProgress())
        {
            rb_player.velocity = new Vector3(movement.x * sprintSpeed, movement.y * jumpingPower, movement.z * sprintSpeed);
        }
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






     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
            Debug.Log("isGrounded");
        }
      
    }
  
}


