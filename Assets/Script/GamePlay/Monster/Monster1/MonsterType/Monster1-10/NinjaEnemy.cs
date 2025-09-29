using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : MonsterBase {
    public float jumpForce = 6f;
    private bool hasAttacked = false;

    protected override void Move() {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        if (!hasAttacked && Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position) < 5f) {
            rb.AddForce(new Vector2(-2f, jumpForce), ForceMode2D.Impulse);
            if(anim != null) anim.SetTrigger("Attack");
            hasAttacked = true;
        }
    }
}
