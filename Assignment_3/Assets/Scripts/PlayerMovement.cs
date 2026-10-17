using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour, IDamageable
{
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;
    public float friction = 0f;
    public float groundOffset = 1f;
    public static PlayerMovement Instance;
    private Rigidbody rb;
    private PlayerInput input;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Vector3 moveDirection;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new PlayerInput();
        moveAction = input.Player.Movement;
        jumpAction = input.Player.Jump;
        Instance = this;
    }

    void OnEnable()
    {
        input.Player.Enable();
    }
    void OnDisable()
    {
        input.Player.Disable();
    }
    void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        moveDirection = (transform.forward * move.y + transform.right * move.x).normalized;
        if (jumpAction.WasPressedThisFrame() && IsGrounded())
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void FixedUpdate()
    {
        rb.AddForce(moveDirection * moveSpeed / 10f, ForceMode.Impulse);
        rb.linearVelocity = new Vector3(
        rb.linearVelocity.x * (100f - friction) / 100f,
        rb.linearVelocity.y - gravity / 10f,
        rb.linearVelocity.z * (100f - friction) / 100f
        );
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundOffset + 0.15f);
    }
}