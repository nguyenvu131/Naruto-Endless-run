using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJump : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;        // Tốc độ chạy ngang
    public float jumpForce = 7f;        // Lực nhảy

    [Header("Ground Check")]
    public Transform groundCheck;       // Vị trí check đất (thường đặt dưới chân)
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;       // Layer của đất

    private Rigidbody2D rb;
    private Animator anim;

    private bool isGrounded = false;
    private bool isDead = false;

    // ---- Cờ kiểm soát Jump ----
    private bool canJump = true;        
    private float jumpCooldown = 0.1f;  // delay nhỏ sau khi chạm đất
    private float lastJumpTime = -1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // Auto-run sang phải
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        // Kiểm tra chạm đất
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Nếu vừa chạm đất => reset lại quyền nhảy sau cooldown
        if (isGrounded && !wasGrounded)
        {
            Invoke("EnableJump", jumpCooldown);
        }

        // Input Jump
        if (canJump && isGrounded &&
            (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0)))
        {
            canJump = false; // khóa ngay khi nhảy
            rb.velocity = new Vector2(rb.velocity.x, 0f); 
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            if (anim != null)
                anim.SetTrigger("Jump");
        }

        // Update animation Run
        if (anim != null)
            anim.SetBool("Run", isGrounded && rb.velocity.x > 0);
    }

    void EnableJump()
    {
        canJump = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;

        if (anim != null)
            anim.SetTrigger("Die");

        Debug.Log("Player Died!");
        // TODO: GameManager.GameOver();
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}