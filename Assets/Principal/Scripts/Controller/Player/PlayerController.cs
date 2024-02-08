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
    private float jumpingPower = 5f;
    private float rotationSpeed = 2f;

    private bool isJumping;
    private bool isGrounded;

    PlayerMap playerMap;

    public PlayerInput playerInput;

    void Start()
    {
        
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
        Jump();
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
    public void RotateCamera()
    {
        Vector2 rotationInput = playerMap.Player.Camera.ReadValue<Vector2>();        
        cameraPivot.Rotate(Vector3.up, rotationInput.x * rotationSpeed, Space.World);
    }


    public void Move()
    {
        Vector3 movement = playerMap.Player.Movement.ReadValue<Vector3>();
        rb_player.velocity = new Vector3(movement.x * moveSpeed,0, movement.z * moveSpeed);

        if (playerInput.actions["Sprint"].IsInProgress())
        {
            rb_player.velocity = new Vector3(movement.x * sprintSpeed, movement.y * jumpingPower, movement.z * sprintSpeed);
        }
    }

    //private void RecupeObject()
    //{

    //}

    public void Jump()
    {
        if (isGrounded && playerInput.actions["Jump"].triggered)
        {
            rb_player.AddForce(Vector3.up * jumpingPower, ForceMode.Impulse);
            isJumping = true;
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
