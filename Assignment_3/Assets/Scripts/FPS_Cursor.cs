using UnityEngine;
using UnityEngine.InputSystem;

public class FPS_Cursor : MonoBehaviour
{
    public float mouseSensitivity = 0.3f;
    public Camera playerCamera;
    float xRotation = 0f;
    private PlayerInput input;
    private InputAction lookAction;
    private void Awake()
    {
        input = new PlayerInput();
        lookAction = input.Player.Look;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }
    private void OnDisable()
    {
        input.Player.Disable();
    }
    private void Update()
    {
        Vector2 lookDelta = lookAction.ReadValue<Vector2>();
        float mouseX = lookDelta.x * mouseSensitivity;
        float mouseY = lookDelta.y * mouseSensitivity;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
