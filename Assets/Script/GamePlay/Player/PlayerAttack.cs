using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Player {

	private PlayerStats playerStats;

    [Header("Melee Attack")]
    public float meleeRate = 1f;
    public float meleeRange = 3f;
    public LayerMask monsterLayer;
    public LayerMask bossLayer; // Thêm layer cho boss

    [Header("Ranged Attack")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float fireRange = 5f;
    public LayerMask enemyLayer;
    public LayerMask bossFireLayer; // Layer boss cho ranged attack

    private float nextMeleeTime = 0f;
    private float fireCooldown = 0f;

    void Update()
    {
        // --- MELEE ATTACK ---
        if (Time.time >= nextMeleeTime)
        {
            AutoMelee();
            nextMeleeTime = Time.time + 1f / meleeRate;
        }

        // --- RANGED ATTACK ---
        fireCooldown -= Time.deltaTime;
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, fireRange, enemyLayer | bossFireLayer);
        if (enemy != null && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void AutoMelee()
    {
        // Check monsters
        Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(transform.position, meleeRange, monsterLayer);
        foreach (Collider2D hit in hitMonsters)
        {
            MonsterComponent monsterComp = hit.GetComponent<MonsterComponent>();
            if (monsterComp != null)
            {
                float dmg = playerStats.CalculateDamage();
                monsterComp.monsterStats.TakeDamage(dmg);
                Debug.Log("Hit Monster (Melee) Damage: " + dmg);
            }
        }

        // Check boss
        Collider2D[] hitBoss = Physics2D.OverlapCircleAll(transform.position, meleeRange, bossLayer);
        foreach (Collider2D hit in hitBoss)
        {
            Boss bossComp = hit.GetComponent<Boss>();
            if (bossComp != null)
            {
                float dmg = playerStats.CalculateDamage();
                bossComp.TakeDamage(dmg);
                Debug.Log("Hit Boss (Melee) Damage: " + dmg);
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = new Vector2(10f, 0f); // bắn sang phải
        }

        // Gắn damage + target cho bullet (có thể nâng cao)
        BulletPlayer bulletComp = bullet.GetComponent<BulletPlayer>();
        if (bulletComp != null)
        {
            bulletComp.playerStats = playerStats;
            bulletComp.enemyLayer = enemyLayer;
            bulletComp.bossLayer = bossFireLayer; // gắn bossLayer cho bullet
        }

        Debug.Log("Player Auto Shoot!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fireRange);
    }
}
