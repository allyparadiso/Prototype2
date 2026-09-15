using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float gravity = -9.81f;

    public Transform cameraTarget;
    public float lookSensitivity = 1.0f;
    public float topClamp = 85f;
    public float bottomClamp = -85f;

    private GameManager manager;
    private CharacterController _controller;
    private Vector2 _moveInput;
    private Vector2 _lookInput;

    private float _cinemachineTargetPitch;
    private float _verticalVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        manager = GameManager.Instance;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            manager.TriggerRandomEffect();
        }
    }
    private void LateUpdate()
    {
        HandleRotation();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        if (_verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }

        Vector3 moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;

        _verticalVelocity += gravity * Time.deltaTime;
        moveDirection.y = _verticalVelocity;

        _controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (_lookInput.sqrMagnitude >= 0.01f)
        {
            _cinemachineTargetPitch -= _lookInput.y * lookSensitivity;
            _cinemachineTargetPitch = Mathf.Clamp(_cinemachineTargetPitch, bottomClamp, topClamp);
            cameraTarget.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0f, 0f);

            transform.Rotate(Vector3.up * (_lookInput.x * lookSensitivity));
        }
    }
}
