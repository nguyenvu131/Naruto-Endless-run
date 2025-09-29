using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonsterBase {
    protected override void Move() {
        rb.velocity = new Vector2(-moveSpeed * 1.5f, rb.velocity.y);
        transform.Rotate(Vector3.forward * -500 * Time.deltaTime);
    }
}