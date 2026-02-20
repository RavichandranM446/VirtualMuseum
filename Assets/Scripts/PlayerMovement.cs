using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;
    public Camera playerCamera;
    public Animator anim;

    private CharacterController controller;
    private float xRotation = 0f;
    private Vector3 velocity;
    private bool isGrounded;
    private float groundCheckDistance = 0.3f; // better ground detection
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- Ground Check ---
        isGrounded = controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            if (isJumping)
            {
                isJumping = false;
                if (anim != null) anim.SetBool("isJumping", false);
            }
        }

        // --- Movement Input ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        bool isMoving = move.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // --- Gravity ---
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // --- Mouse Look ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // --- Jump ---
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
            if (anim != null)
            {
                anim.SetBool("isJumping", true);
                anim.SetTrigger("JumpTrigger");
            }
        }

        // --- Animation Control ---
        if (anim != null)
        {
            anim.SetBool("isWalking", isMoving && !isRunning && !isJumping);
            anim.SetBool("isRunning", isMoving && isRunning && !isJumping);
        }
    }
}
