using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.81f;
    
    private CharacterController controller;
    private Transform playerTransform;

    [SerializeField] private LayerMask ground;
    private Vector3 fallVelocity;
    private const float groundCheckRadius = 0.1f;
    
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerTransform = gameObject.transform;
    }

    void LateUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = Vector3.Normalize(playerTransform.right * x + playerTransform.forward * y);
        
        if (IsGrounded && IsStillFalling)
        {
            fallVelocity.y = 0f;
        }
        else
        {
            fallVelocity.y += gravity * Time.deltaTime;
        }

        controller.Move(Time.deltaTime * (move * speed + fallVelocity));
    }

    private bool IsGrounded => Physics.CheckSphere(playerTransform.position, groundCheckRadius, ground);

    private bool IsStillFalling => fallVelocity.y < 0;
}
