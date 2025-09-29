using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone : MonsterBase {
    public GameObject clonePrefab;
    private bool cloned = false;

    protected override void Move() {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        if (!cloned && currentHP <= maxHP / 2) {
            Instantiate(clonePrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
            Instantiate(clonePrefab, transform.position + Vector3.down * 1f, Quaternion.identity);
            cloned = true;
        }
    }
}
