using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonsterBase {
    private float attackCooldown = 3f;
    private float timer;

    protected override void Move() {
        rb.velocity = new Vector2(-moveSpeed * 0.5f, 0);
        timer += Time.deltaTime;
        if(timer >= attackCooldown) {
            int rand = Random.Range(0, 2);
            if(rand == 0) {
                if(anim != null) anim.SetTrigger("Punch");
            } else {
                if(anim != null) anim.SetTrigger("Slash");
            }
            timer = 0;
        }
    }
}
