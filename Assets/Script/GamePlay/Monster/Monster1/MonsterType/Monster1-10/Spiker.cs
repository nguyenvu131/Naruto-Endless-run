using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiker : MonsterBase {
    private bool charge = false;

    protected override void Move() {
        if (!charge) {
            // cảnh báo (có thể anim)
            charge = true;
            Invoke("ChargeAttack", 1f);
        }
    }

    void ChargeAttack() {
        rb.velocity = new Vector2(-moveSpeed * 2f, 0);
    }
}