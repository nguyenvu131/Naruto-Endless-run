using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour 
{
    [Header("Monster Info")]
    protected MonsterStats stats; // Sử dụng Scriptable-like MonsterStats
    public MonsterType monsterType;
    public float moveSpeed = 2f;
    public int maxHP = 1;
    public int damage = 1;
    public GameObject deathEffect;    // hiệu ứng khi chết
    public GameObject hitEffect;      // hiệu ứng khi trúng player
    public GameObject dropItemPrefab; // item hoặc coin spawn khi chết

    [Header("Runtime")]
    public int currentHP;
    protected bool isDead = false;

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
        // Move(); // Quái chỉ di chuyển
    }

    // Xử lý di chuyển, mỗi quái override riêng
    // protected abstract void Move();

    // Nhận sát thương
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

        // if(anim != null) anim.SetTrigger("Die");

        if(stats != null && stats.deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(stats.deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        // Gọi các reward
        PlayerStatsManager.Instance.PlayerGainEXP(stats.expReward);		
        DropItem();
		
        Destroy(gameObject);
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
