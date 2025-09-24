using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerAI_Jump : MonoBehaviour {

	public Transform leader;
    public float followDistance = 2f;
    public float moveSpeed = 5f;

    public LayerMask obstacleLayer;
    public float obstacleCheckDistance = 1.5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (leader == null) return;

        FollowLeader();
        CheckObstacleAndAvoid();
    }

    void FollowLeader()
    {
        // targetPos = vị trí sau leader một khoảng followDistance
        Vector3 targetPos = leader.position - leader.right * followDistance;
        Vector3 newPos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
    }

    void CheckObstacleAndAvoid()
    {
        // Raycast kiểm tra obstacle phía trước
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, obstacleCheckDistance, obstacleLayer);

        if (hit.collider != null && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * obstacleCheckDistance);
    }
}
