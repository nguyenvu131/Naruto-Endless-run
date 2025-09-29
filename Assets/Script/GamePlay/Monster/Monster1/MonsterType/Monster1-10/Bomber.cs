using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonsterBase {
    public GameObject bombPrefab;
    public float dropRate = 2f;
    private float timer;

    protected override void Move() {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        timer += Time.deltaTime;
        if(timer >= dropRate) {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
