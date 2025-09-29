using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour 
{
    [Header("Monster Info")]
	private MonsterStats stats; // Sử dụng Scriptable-like MonsterStats
    public MonsterType monsterType;
    public float moveSpeed = 2f;
    public int maxHP = 1;
    public int damage = 1;
    public GameObject deathEffect; // hiệu ứng khi chết
    public GameObject hitEffect;   // hiệu ứng khi trúng player
    public GameObject dropItemPrefab; // item hoặc coin spawn khi chết

    [Header("Runtime")]
    public int currentHP;
    private bool isDead = false;

    [Header("Attack Settings")]
    public bool canShoot = false;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootCooldown = 2f;
    private float shootTimer = 0f;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected virtual void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(stats != null)
        {
            stats.InitStats(stats.type, stats.level); // Khởi tạo theo loại + level
        }
    }

    protected virtual void Update() 
    {
        if(isDead) return;

        Move();

        if(canShoot)
        {
            shootTimer += Time.deltaTime;
            if(shootTimer >= shootCooldown)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    // Xử lý di chuyển, mỗi quái override riêng
    protected abstract void Move();

    // Nhận sát thương
    // Nhận sát thương, delegate về MonsterStats
    public virtual void TakeDamage(float dmg) 
    {
        if(isDead || stats == null) return;

        stats.TakeDamage(dmg);

        if(stats.currentHP <= 0)
        {
            Die();
        }
    }

    // Chết
    protected virtual void Die() 
    {
        if(isDead) return;

        isDead = true;

        if(anim != null) anim.SetTrigger("Die");

        if(stats != null && stats.deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(stats.deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        // Gọi các reward
        PlayerStatsManager.Instance.PlayerGainEXP(stats.expReward);		
		DropItem();
		
        Destroy(gameObject, 0.5f);
    }

    // Tấn công player khi chạm
    protected virtual void AttackPlayer(PlayerStats playerStats) 
    {
        if(playerStats != null && stats != null) 
        {
            float dmg = stats.CalculateDamage();
            playerStats.TakeDamage(dmg);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        if(playerStats != null) 
        {
            AttackPlayer(playerStats);
        }
    }

    // Bắn đạn nếu quái có thể bắn
    protected virtual void Shoot()
    {
        if(bulletPrefab != null && shootPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            MonsterBullet mb = bullet.GetComponent<MonsterBullet>();
            if(mb != null)
            {
                mb.Init(stats.currentDamage, transform.right); // Bắn theo hướng quái
            }
        }
    }

    // Drop item hoặc coin khi chết
    protected virtual void DropItem()
    {
        if(dropItemPrefab != null)
        {
            Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
        }
    }
}
