using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Movement")]
    public float moveSpeed = 5f;            // tốc độ chạy ngang
    public float jumpForce = 8f;            // lực nhảy
    public float gravity = -20f;            // trọng lực
    public float maxFallSpeed = -25f;       // tốc độ rơi tối đa

    [Header("Jump")]
    public int maxJumps = 2;                // double jump
    private int jumpsRemaining;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded = false;

    [Header("Coyote Time")]
    public bool allowCoyoteTime = true;
    public float coyoteTime = 0.12f;
    private float coyoteTimer = 0f;

    [Header("Start Delay")]
    public float waitTime = 1.5f;   // thời gian chờ trước khi chạy
    private bool isRunning = false;

    [Header("State")]
    private float verticalVelocity = 0f;
    private bool isDead = false;

    [Header("Animation")]
    private Animator anim;

    public static PlayerController Instance;

    // Invincible flag
    private bool _isInvincible = false;

    void Start()
    {
        Instance = this;
        anim = GetComponent<Animator>();

        jumpsRemaining = maxJumps;

        // idle trước khi chạy
        if (anim != null) anim.Play("naruto_idle");
        Invoke("StartRunning", waitTime);
    }

    void Update()
    {
        if (isDead) return;

        HandleGroundCheck();
        HandleMovement();
        HandleAnimation();
    }

    void HandleGroundCheck()
    {
        if (groundCheckPoint == null) return;

        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            jumpsRemaining = maxJumps;
            coyoteTimer = coyoteTime;
            if (!wasGrounded && verticalVelocity < 0f)
                verticalVelocity = 0f;
        }
        else
        {
            if (allowCoyoteTime)
                coyoteTimer -= Time.deltaTime;
            else
                coyoteTimer = 0f;
        }
    }

    void HandleMovement()
    {
        Vector3 pos = transform.position;

        // --- Auto run ngang ---
        if (isRunning)
            pos.x += moveSpeed * Time.deltaTime;

        // --- Jump input ---
        bool jumpPressed = Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0);
        if (jumpPressed)
        {
            bool canUseCoyote = allowCoyoteTime && coyoteTimer > 0f;
            if (isGrounded || canUseCoyote)
            {
                verticalVelocity = jumpForce;
                jumpsRemaining = maxJumps - 1;
                coyoteTimer = 0f;
                if (anim != null) anim.SetTrigger("Jump");
            }
            else if (jumpsRemaining > 0)
            {
                verticalVelocity = jumpForce;
                jumpsRemaining--;
                if (anim != null) anim.SetTrigger("Jump");
            }
        }

        // --- Gravity ---
        verticalVelocity += gravity * Time.deltaTime;
        if (verticalVelocity < maxFallSpeed) verticalVelocity = maxFallSpeed;

        // --- Apply movement ---
        pos.y += verticalVelocity * Time.deltaTime;
        transform.position = pos;
    }

    void HandleAnimation()
    {
        if (anim != null)
        {
            anim.SetBool("Run", isGrounded && isRunning);
        }
    }

    public void StopRunning()
    {
        isRunning = false;
        if (anim != null) anim.SetBool("Run", false);
    }

    public void ResumeRunning()
    {
        isRunning = true;
    }

    void StartRunning()
    {
        isRunning = true;
        if (anim != null)
            anim.SetBool("Run", true);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        isRunning = false;
        verticalVelocity = 0f;
        if (anim != null) anim.SetTrigger("Die");
    }

    public bool isInvincible
    {
        get { return _isInvincible; }
        set { _isInvincible = value; }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PausePlayer"))
        {
            StopRunning();
            Debug.Log("Encounter Boss! Player stopped running.");
        }

        if ((col.CompareTag("Enemy") || col.CompareTag("Obstacle") || col.CompareTag("Boss")) && !_isInvincible)
        {
            Die();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
