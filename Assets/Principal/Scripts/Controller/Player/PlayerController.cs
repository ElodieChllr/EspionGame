using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb_player;

    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;

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
        Jump();
    }

    private void Move()
    {
        Vector3 movement = playerMap.Player.Movement.ReadValue<Vector3>();
        rb_player.velocity = new Vector3(movement.x * moveSpeed, 0, movement.z * moveSpeed);
    }

    private void Jump()
    {
        Vector3 movement = playerMap.Player.Movement.ReadValue<Vector3>();
        rb_player.velocity = new Vector3(0, movement.y * jumpSpeed, 0); 
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
