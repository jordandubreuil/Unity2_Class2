using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Set the speed of the player movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.8f;
    public float rotationSmoothTime = 0.1f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    [Header("References")]
    public Transform cameraTransform;


    //private variables
    CharacterController _characterController;
    Vector3 _velocity;
    Vector2 _moveInput;
    bool _isGrounded;
    bool _isJumping;
    bool _sprintHeld;
    float _jumpCooldown = 0f;
    bool _jumpPressed;
    float _rotationVelocity;



    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        //Auto find the camera in scene if not assigned
        if(cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    // Set up inputs via messaging
    /// <summary>
    /// This handles the movement of the the character via input.
    /// </summary>
    /// <param name="value">Input value passed from the input system</param>
    public void OnMove(InputValue value)
    {
        Debug.Log(value.Get<Vector2>());
        _moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value) 
    {
        if (value.isPressed)
        {
            _jumpPressed = true;
        }
    }

    public void OnSprint(InputValue value) 
    {
        _sprintHeld = !_sprintHeld;
    }



    // Update is called once per frame
    void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    void HandleMovement()
    {
        if (_moveInput.sqrMagnitude < 0.01f)
        {
            return;
        }

        float speed = _sprintHeld? sprintSpeed : moveSpeed;

        //Camera related directional rotation
        float targetAngle = Mathf.Atan2(_moveInput.x, _moveInput.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

        //Smoothed rotation toward movement direction
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationVelocity, rotationSmoothTime);

        transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;// original new Vector3(_moveInput.x, 0f, _moveInput.y);
        _characterController.Move(moveDir.normalized * speed * Time.deltaTime);
    }

    void HandleGroundCheck()
    {
        if (_jumpCooldown > 0f)
        {
            _jumpCooldown -= Time.deltaTime;
            _isGrounded = false;
            return;
        }
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0) {
            _velocity.y = -2f;
            _isJumping = false;
        }
    }

    void HandleJump()
    {
        if (_jumpPressed && _isGrounded && !_isJumping)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            _isJumping = true;
            _jumpCooldown = 0.5f;
        }

        _jumpPressed = false;
    }

    /// <summary>
    /// The function handles gravity for the character controller.
    /// </summary>
    void ApplyGravity()
    {
        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(new Vector3(0, _velocity.y, 0) * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
