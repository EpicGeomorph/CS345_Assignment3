using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour,IDamagable
{
    public float moveSpeed = 15;
    public float jumpForce = 25;
    public Camera playerCamera;
    private Rigidbody rigidBody;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float friction = 10;
    [SerializeField] private float gravity = 3;
    private float offset = 1;
    public static PlayerMovement Instance;

    void Awake(){
        Instance = this;
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        // Asked ChatGPT for this, since the game kept throwing an "InvalidOperationException" when using the given code and I was unsure of how to fix it
        float horzMove = 0f;
        float vertMove = 0f;

        if (Keyboard.current != null)
        {
            // Horizontal movement (A/D or Left/Right arrows)
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                horzMove = -1f;
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                horzMove = 1f;

            // Vertical movement (W/S or Up/Down arrows)
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                vertMove = -1f;
            else if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                vertMove = 1f;

            // Jump with Space
            if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
            {
                rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }

        moveDirection = (transform.forward * vertMove + transform.right * horzMove).normalized;

        //float horzMove = Input.GetAxisRaw("Horizontal");
        //float vertMove = Input.GetAxisRaw("Vertical");
        //moveDirection = (transform.forward * vertMove + transform.right * horzMove).normalized;

        //if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
        //    rigidBody.AddForce(transform.up * jumpForce,ForceMode.Impulse);
        //}
    }

    void FixedUpdate() {
        rigidBody.AddForce(moveDirection.normalized *moveSpeed/10,ForceMode.Impulse);
        rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x*(100-friction)/100,rigidBody.linearVelocity.y-gravity/10,rigidBody.linearVelocity.z*(100-friction)/100);
    }

    private bool IsGrounded(){
        return Physics.Raycast(transform.position,Vector3.down,offset + .15f);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Implement this");
    }
}