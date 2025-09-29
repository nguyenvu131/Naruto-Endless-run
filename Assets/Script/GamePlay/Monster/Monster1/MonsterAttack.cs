using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : Monster {

    public float attackCooldown = 2f;           // Thời gian hồi đòn
    private float lastAttackTime = 0f;

    public float attackRange = 5f;              // Phạm vi bắn
    public Transform firePoint;                 // Vị trí bắn đạn
    public GameObject bulletPrefab;             // Prefab đạn quái

    private Transform playerTarget;             // Player để theo dõi

    protected int attackSpeed = 5;

    void Start()
    {
        if (stats == null)
            stats = GetComponent<MonsterStats>();

        // tìm Player trong scene theo tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerTarget = playerObj.transform;
    }

    void Update()
    {
        if (playerTarget == null) return;

        float distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance <= attackRange)
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Shoot();
            lastAttackTime = Time.time;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null && playerTarget != null)
        {
            Vector2 dir = (playerTarget.position - firePoint.position).normalized;
            rb.velocity = dir * attackSpeed; // attackSpeed dùng làm tốc độ bay đạn
        }

        // Gán damage cho bullet
        MonsterBullet bulletScript = bullet.GetComponent<MonsterBullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = stats.CalculateDamage();
        }

        Debug.Log(stats.monsterName + " bắn đạn!");
    }

    // ===================== Vẽ Gizmos =====================
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
