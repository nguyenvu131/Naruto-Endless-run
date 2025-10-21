using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {

	[Header("Bullet Settings")]
    public float speed = 10f;
    public float lifeTime = 3f;
    public LayerMask enemyLayer;

    [Header("References")]
    public PlayerStats playerStats;
    public PlayerAttack ownerAttack; // người bắn

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Nếu có Rigidbody2D, cho đạn bay về phía trước
        if (rb != null)
            rb.velocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra layer hợp lệ
        if (((1 << other.gameObject.layer) & enemyLayer) == 0)
            return;

        // Lấy đối tượng MonsterBase (cha của Monster, Boss,...)
        MonsterBase monster = other.GetComponent<MonsterBase>();
        if (monster != null)
        {
            float dmg = playerStats != null ? playerStats.CalculateDamage() : 10f;
            monster.TakeDamage(dmg);

            // Gọi callback ngược lại PlayerAttack
            if (ownerAttack != null)
                ownerAttack.OnBulletDestroyed(this);

            Destroy(gameObject);
            return;
        }

        // Nếu quái không có script MonsterBase → có thể là Boss riêng biệt
        Boss boss = other.GetComponent<Boss>();
        if (boss != null)
        {
            float dmg = playerStats != null ? playerStats.CalculateDamage() : 10f;
            boss.TakeDamage(dmg);

            if (ownerAttack != null)
                ownerAttack.OnBulletDestroyed(this);

            Destroy(gameObject);
        }
    }
}
