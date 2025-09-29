using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonsterBase 
{
    public float jumpForce = 5f;
    private bool isGrounded = true;

    protected override void Move() {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        if (isGrounded) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
        }
    }
}
