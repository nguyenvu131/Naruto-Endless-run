using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1	Straight Move	Quái đi thẳng từ phải sang trái
//2	Slow-Fast Move	Quái đi chậm rồi tăng tốc khi gần player
//3	Zigzag Move	Quái di chuyển hình sin (zigzag)
//4	Wave Move	Quái bay theo đường parabol nhỏ
//5	Dash Attack	Quái dừng 1s rồi lao nhanh về phía player
//6	Hover Move	Quái bay lơ lửng qua lại trong phạm vi nhỏ
//7	Random Stop	Quái vừa đi vừa thỉnh thoảng dừng lại
//8	Jump Move	Quái nhảy nhịp nhàng (dùng AddForce)
//9	Follow Player	Quái di chuyển chậm, hướng theo vị trí player
//10 Smart Combo	Quái kết hợp zigzag + dash ngẫu nhiên

public class MonsterMove : MonoBehaviour {

	public MonsterMoveType moveType = MonsterMoveType.Straight;
    public float moveSpeed = 2f;
    public float zigzagAmplitude = 1f;
    public float zigzagFrequency = 2f;

    private Rigidbody2D rb;
    private Transform player;
    private Vector3 startPos;
    private float dashCooldown = 2f;
    private float dashTimer = 0f;
    private bool isStopping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position;
    }

    void Update()
    {
        switch (moveType)
        {
            case MonsterMoveType.Straight:
                StraightMove();
                break;
            case MonsterMoveType.SlowFast:
                SlowFastMove();
                break;
            case MonsterMoveType.Zigzag:
                ZigzagMove();
                break;
            case MonsterMoveType.Wave:
                WaveMove();
                break;
            case MonsterMoveType.Dash:
                DashMove();
                break;
            case MonsterMoveType.Hover:
                HoverMove();
                break;
            case MonsterMoveType.RandomStop:
                RandomStopMove();
                break;
            case MonsterMoveType.Jump:
                JumpMove();
                break;
            case MonsterMoveType.Follow:
                FollowMove();
                break;
            case MonsterMoveType.SmartCombo:
                SmartComboMove();
                break;
        }
    }

    // ========== Level 1 ==========
    void StraightMove()
    {
        rb.velocity = Vector2.left * moveSpeed;
    }

    // ========== Level 2 ==========
    void SlowFastMove()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        float speed = (distance < 3f) ? moveSpeed * 2f : moveSpeed;
        rb.velocity = Vector2.left * speed;
    }

    // ========== Level 3 ==========
    void ZigzagMove()
    {
        float y = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        rb.velocity = new Vector2(-moveSpeed, y);
    }

    // ========== Level 4 ==========
    void WaveMove()
    {
        float y = Mathf.Sin((transform.position.x - startPos.x) * zigzagFrequency) * zigzagAmplitude;
        rb.MovePosition(transform.position + new Vector3(-moveSpeed * Time.deltaTime, y * Time.deltaTime, 0));
    }

    // ========== Level 5 ==========
    void DashMove()
    {
        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0f)
        {
            rb.velocity = Vector2.left * (moveSpeed * 4f);
            dashTimer = dashCooldown;
        }
        else
        {
            rb.velocity = Vector2.left * moveSpeed;
        }
    }

    // ========== Level 6 ==========
    void HoverMove()
    {
        float y = Mathf.Sin(Time.time * 2f) * 0.5f;
        rb.MovePosition(transform.position + new Vector3(-moveSpeed * Time.deltaTime, y * Time.deltaTime, 0));
    }

    // ========== Level 7 ==========
    void RandomStopMove()
    {
        if (!isStopping && Random.value < 0.01f)
        {
            isStopping = true;
            rb.velocity = Vector2.zero;
            Invoke("ResumeMove", 1f);
        }
        else if (!isStopping)
        {
            rb.velocity = Vector2.left * moveSpeed;
        }
    }

    void ResumeMove()
    {
        isStopping = false;
    }

    // ========== Level 8 ==========
    void JumpMove()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        if (Random.value < 0.01f && rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }

    // ========== Level 9 ==========
    void FollowMove()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(-moveSpeed, direction.y * moveSpeed);
    }

    // ========== Level 10 ==========
    void SmartComboMove()
    {
        if (Random.value < 0.5f)
            ZigzagMove();
        else
            DashMove();
    }
}
