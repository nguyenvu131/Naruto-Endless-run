using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5f;        // Tốc độ chạy
    public float jumpForce = 7f;        // Lực nhảy
    private Rigidbody2D rb;
    private bool isGrounded = false;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Animator anim;
    private bool isDead = false;

    public static PlayerController Instance;
    protected bool isRunning = true;    // Player auto-run
	protected bool _isInvincible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Instance = this;
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Input nhảy check ngay trong FixedUpdate
        if (isGrounded && (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0)))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset y velocity
            rb.AddForce(new Vector2(0f, jumpForce));
        }
		
		// Attack (tạm thời log ra console)
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Player Attack!");
            // anim.SetTrigger("Attack");
        }
		
		// Skill
		if (Input.GetKeyDown(KeyCode.Alpha1))
			GetComponent<SkillPowerAttack>().TryUseSkill();

		if (Input.GetKeyDown(KeyCode.Alpha2))
			GetComponent<SkillDash>().TryUseSkill();

		if (Input.GetKeyDown(KeyCode.Alpha3))
			GetComponent<SkillFireBreath>().TryUseSkill();

        anim.SetBool("Run", isGrounded && rb.velocity.x > 0 && isRunning);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Nếu gặp Boss thì dừng chạy
        if (col.CompareTag("Boss"))
        {
            StopRunning();
            Debug.Log("Encounter Boss! Player stopped running.");
        }
    }

    public void StopRunning()
    {
        isRunning = false;           // Tắt auto-run
		rb.velocity = Vector2.zero;  // Dừng ngay lập tức
		anim.SetBool("Run", false);  // Tắt animation chạy
    }

    public void ResumeRunning()
    {
        isRunning = true;
    }

    void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Die");
        // GameManager.instance.GameOver();
    }
	
	public bool isInvincible
    {
        get { return _isInvincible; }
        set { _isInvincible = value; }
    }
}
