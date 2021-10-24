using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float airDragMultiplier = 0.3f;
    [SerializeField] private KeyBindings keyBinds;
    [SerializeField] private LayerMask ground;

    private Transform playerTransform;
    private Rigidbody playerBody;

    private float baseDrag;
    private bool isGrounded;
    private Vector2 normalizedInput;
    private Vector3 moveDirection;
    private const float groundCheckRadius = 0.1f;
    

    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        playerTransform = gameObject.transform;
        baseDrag = playerBody.drag;
    }

    void Update()
    {
        normalizedInput = GetMoveInput(keyBinds).normalized;
        moveDirection = playerTransform.right * normalizedInput.x + playerTransform.forward * normalizedInput.y;
        
        isGrounded = Physics.CheckSphere(playerTransform.position, groundCheckRadius, ground);
        
        AdjustDrag();
        
        if (HasJumpInput(keyBinds) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void AdjustDrag()
    {
        playerBody.drag = isGrounded ? baseDrag : baseDrag * airDragMultiplier;
    }

    private void Move()
    {
        if (isGrounded)
            playerBody.AddForce(moveDirection * speed, ForceMode.Acceleration);
    }

    private void Jump()
    {
        playerBody.AddForce(playerTransform.up * jumpForce, ForceMode.Impulse);
    }

    private Vector2 GetMoveInput(KeyBindings keyBindings)
    {
        float x = 0f;
        float y = 0f;

        if (keyBindings != null)
        {
            // Manual keybinds
            bool isRightKeyPressed = Input.GetKey(keyBindings.StrafeRight);
            bool isLeftKeyPressed = Input.GetKey(keyBindings.StrafeLeft);
            bool isForwardKeyPressed = Input.GetKey(keyBindings.Forward);
            bool isBackwardKeyPressed = Input.GetKey(keyBindings.Backward);

            x = Convert.ToInt32(isRightKeyPressed);
            x -= Convert.ToInt32(isLeftKeyPressed);
            y = Convert.ToInt32(isForwardKeyPressed);
            y -= Convert.ToInt32(isBackwardKeyPressed);
        }
        else
        {
            // Or use Unity built-in axis
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }

        return new Vector2(x, y);
    }

    private bool HasJumpInput(KeyBindings keyBindings)
    {
        return keyBindings != null ? Input.GetKeyDown(keyBindings.Jump) : Input.GetButtonDown("Jump");
    }
}
