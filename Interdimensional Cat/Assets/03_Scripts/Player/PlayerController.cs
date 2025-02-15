using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;

    private Vector2 moveInput;
    private float movement;

    [Space]

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float gravityScale = 4f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;

    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    [Space]

    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool isFacingRight;

    private bool isGrounded;

    [Space]

    [Header("Camera")]
    [SerializeField] private GameObject cameraFollow;
    private float fallSpeedYDampingChangeThreshold;



    private Rigidbody2D rb;
    private CameraFollowObject cameraFollowObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraFollowObject = cameraFollow.GetComponent<CameraFollowObject>();

        rb.gravityScale = gravityScale;
        isFacingRight = true;

        fallSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedYDampingChangeThreshold;
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        float targetSpeed = moveInput.x * moveSpeed;
        float speedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        movement = speedDif * accelRate * Time.deltaTime;


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        } else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        } else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (rb.linearVelocity.y < fallSpeedYDampingChangeThreshold && !CameraManager.instance.isLerpingYDamping && !CameraManager.instance.lerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        if (rb.linearVelocity.y >= 0f && !CameraManager.instance.isLerpingYDamping && !CameraManager.instance.lerpedFromPlayerFalling)
        {
            CameraManager.instance.lerpedFromPlayerFalling = false;

            CameraManager.instance.LerpYDamping(false);
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x + movement, rb.linearVelocity.y);

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (moveInput.x > 0 || moveInput.x < 0)
        {
            TurnCheck();
        }
    }

    private void TurnCheck()
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            Turn();
        } 
        
        else if (moveInput.x < 0 && isFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if (isFacingRight)
        {
            Vector3 rotator = new Vector3 (transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;

            cameraFollowObject.CallTurn();
        } 
        
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;

            cameraFollowObject.CallTurn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            interactable.OnInteract();
        }
    }
}
