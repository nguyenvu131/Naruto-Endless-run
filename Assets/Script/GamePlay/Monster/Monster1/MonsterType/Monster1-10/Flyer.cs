using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonsterBase {
    public bool isHigh = false;
    public float flyHeight = 3f;

    protected override void Move() {
        rb.velocity = new Vector2(-moveSpeed, 0);
        if(isHigh) {
            transform.position = new Vector2(transform.position.x, flyHeight);
        } else {
            transform.position = new Vector2(transform.position.x, 1.2f);
        }
    }
}
