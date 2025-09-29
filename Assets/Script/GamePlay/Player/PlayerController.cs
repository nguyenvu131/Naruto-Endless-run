using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5f;        
    public float jumpForce = 7f;        
    private Rigidbody2D rb;
    private bool isGrounded = false;

    public LayerMask groundLayer;				
    public Transform groundCheck;				
    public float groundCheckRadius = 2.5f;

    private Animator anim;
    private bool isDead = false;

    public static PlayerController Instance;
    protected bool isRunning = false;    // Ban đầu không chạy
    protected bool _isInvincible = false;

    // ---- Jump control ----
    private bool canJump = true;        
    protected float jumpCooldown = 0.1f;  
    protected float lastJumpTime = -1f;

    // ---- Wait before running ----
    public float waitTime = 1.5f; // Thời gian chờ trước khi chạy
	
	// public GameObject bulletPrefab;
	// public Transform firePoint;
	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Instance = this;

        // Bắt đầu chờ rồi mới chạy
        anim.Play("naruto_idle");
        Invoke("StartRunning", waitTime); 
    }

    void FixedUpdate()
    {
        if (isDead) return;

        if (isRunning)
        {
            // Luôn chạy về bên phải
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
		
        // Kiểm tra chạm đất
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !wasGrounded)
            Invoke("EnableJump", jumpCooldown);

        // Jump input
        if (canJump && isGrounded &&
            (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0)))
        {
            // canJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0f); 
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            if (anim != null)
                anim.SetTrigger("Jump");
        }

        // Animation Run
        if (anim != null)
            anim.SetBool("Run", isGrounded && rb.velocity.x > 0 && isRunning);

        // Attack
        // if (Input.GetKeyDown(KeyCode.J))
		// {
            // Debug.Log("Player Attack!");
			// Shoot();
		// }
		
        // Skills
        // if (Input.GetKeyDown(KeyCode.Alpha1))
            // GetComponent<SkillPowerAttack>().TryUseSkill();
        // if (Input.GetKeyDown(KeyCode.Alpha2))
            // GetComponent<SkillDash>().TryUseSkill();
        // if (Input.GetKeyDown(KeyCode.Alpha3))
            // GetComponent<SkillFireBreath>().TryUseSkill();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Obstacle"))
            Die();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PausePlayer"))
        {
            StopRunning();
            Debug.Log("Encounter Boss! Player stopped running.");
        }

        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Obstacle"))
            Die();
    }

    public void StopRunning()
    {
        isRunning = false;           
        rb.velocity = Vector2.zero;  
        anim.SetBool("Run", false);  
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

    void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Die");
    }

    public bool isInvincible
    {
        get { return _isInvincible; }
        set { _isInvincible = value; }
    }

    void EnableJump()
    {
        canJump = true;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
	
	// void Shoot()
	// {
		// if (bulletPrefab != null && firePoint != null)
		// {
			// Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
			// anim.SetTrigger("Attack");
		// }
	// }
}
