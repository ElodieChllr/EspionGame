using UnityEngine;
using UnityEngine.InputSystem;

public class LastPlayerMovement : MonoBehaviour
{
    //public Rigidbody rb;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool grounded;

    [SerializeField] private InputActionReference movementControl;
    [SerializeField] private InputActionReference jumpControl;
    [SerializeField] private InputActionReference sprintControl;

    [SerializeField] private float playerSpeed = 2f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float sprintSpeed = 4f;

    private Transform cameraMain;

    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
        sprintControl.action.Enable();
    }
    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
        sprintControl.action.Disable();
    }
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        controller= GetComponent<CharacterController>();
        cameraMain = Camera.main.transform;
    }

    void Update()
    {
        //grounded = rb.velocity.y <= 0;
        grounded = controller.isGrounded;
        //MovementPlayer();
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        
        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraMain.forward * move.z + cameraMain.right * move.x;
        move.y = 0f;

        //rb.MovePosition(move * Time.deltaTime * playerSpeed);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (jumpControl.action.triggered && grounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        //rb.MovePosition(playerVelocity * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);

        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMain.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        if (sprintControl.action.IsPressed())
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
    }
    //public void MovementPlayer()
    //{

    //}
}
