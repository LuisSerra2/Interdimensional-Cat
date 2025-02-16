using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Death,
    Menu
}

public class PlayerController : MonoBehaviour
{
    public GameState gameState => GameController.Instance.GameState;

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

    [Space]

    [Header("OnDeath")]
    [SerializeField] private CanvasGroup deathPanelCG;

    private Rigidbody2D rb;
    private CameraFollowObject cameraFollowObject;
    private Animator animator;
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = new HealthSystem(1);
    }

    private void OnEnable()
    {
        healthSystem.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        healthSystem.OnDeath -= OnDeath;

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        cameraFollowObject = cameraFollow.GetComponent<CameraFollowObject>();

        rb.gravityScale = gravityScale;
        isFacingRight = true;

        fallSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedYDampingChangeThreshold;

        GameController.Instance.ChangeState(GameState.Intro);
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Intro:

                break;
            case GameState.Playing:
                Inputs();
                if (Input.GetKeyDown(KeyCode.V))
                {
                    TakeHit(1);
                }
                break;
            case GameState.Death:

                break;
            case GameState.Menu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameController.Instance.ChangeState(GameState.Playing);
                    GameController.Instance.OnMenuEvent();
                }
                break;
        }

    }


    private void FixedUpdate()
    {
        switch (gameState)
        {
            case GameState.Intro:
                break;
            case GameState.Playing:
                rb.linearVelocity = new Vector2(rb.linearVelocity.x + movement, rb.linearVelocity.y);

                if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    jumpBufferCounter = 0;
                    coyoteTimeCounter = 0;
                    animator.SetTrigger("Jump");
                    GameController.Instance.PlaySound(SoundType.Jump);
                }

                if (rb.linearVelocity.y < 0)
                {
                    rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }

                if (moveInput.x > 0 || moveInput.x < 0)
                {
                    TurnCheck();
                }
                break;
            case GameState.Menu:
                rb.linearVelocity = Vector2.zero;
                break;
        }


    }

    #region Movement
    private void Inputs()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        float targetSpeed = moveInput.x * moveSpeed;
        float speedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        movement = speedDif * accelRate * Time.deltaTime;

        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            animator.SetBool("isFalling", false);
        } else
        {
            coyoteTimeCounter -= Time.deltaTime;
            if (rb.linearVelocity.y < 0)
            {
                animator.SetBool("isFalling", true);
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        } else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (wasGrounded && !isGrounded && rb.linearVelocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }

        animator.SetBool("isRunning", moveInput.x != 0 && isGrounded);
        animator.SetBool("isGrounded", isGrounded);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameController.Instance.ChangeState(GameState.Menu);
            GameController.Instance.OnMenuEvent();
        }
    }

    private void TurnCheck()
    {
        if ((moveInput.x > 0 && !isFacingRight) || (moveInput.x < 0 && isFacingRight))
        {
            Turn();
        }
    }

    private void Turn()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
        cameraFollowObject.CallTurn();
    }

    #endregion

    public void TakeHit(int damage)
    {
        if (healthSystem.GetCurrentHealth() <= 0) return;
        healthSystem.TakeDamage(damage);
    }

    private void OnDeath()
    {
        StartCoroutine(OnDeathAnimation());
    }

    private IEnumerator OnDeathAnimation()
    {
        GameController.Instance.ChangeState(GameState.Death);

        animator.SetTrigger("Dead");
        GameController.Instance.PlaySound(SoundType.Death);

        yield return new WaitForSeconds(1f);

        deathPanelCG.DOFade(1, 2).OnComplete(() =>
        {
            string sceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(sceneName);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            interactable.OnInteract();
        }
    }
}
