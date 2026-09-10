using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float gravity = -9.81f;

    public Transform cameraTarget;
    public float lookSensitivity = 1.0f;
    public float topClamp = 85f;
    public float bottomClamp = -85f;

    private CharacterController _controller;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
}
