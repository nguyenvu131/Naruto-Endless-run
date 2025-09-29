using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonsterBase 
{
    protected override void Move() {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }
}
