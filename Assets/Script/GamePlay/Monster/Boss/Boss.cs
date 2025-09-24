using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
	
	public BossStats stats;
	
	public int maxHP = 50;
    private float currentHP;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackRate = 2f;
    private float nextAttack;
	
	public float attackRange = 1f; // phạm vi tấn công
    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            Attack();
        }
    }

    void Attack()
    {
        // Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
		if(projectilePrefab != null && firePoint != null)
		{
			GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;
			
			// Set damage từ BossStats
			BossBullet bb = bullet.GetComponent<BossBullet>();
			if(bb != null)
			{
				bb.damage = stats.damage;
			}
		}
    }

    // public void TakeDamage(int damage)
    // {
        // currentHP -= damage;
        // if (currentHP <= 0)
        // {
            // Die();
        // }
    // }
	
	/// <summary>
    /// Boss nhận sát thương từ player
    /// </summary>
    /// <param name="incomingDamage">Damage gốc từ player</param>
    public void TakeDamage(float incomingDamage)
    {
        // Dodge, Critical, hoặc các hiệu ứng khác có thể thêm sau

        // Công thức giáp tuyến tính:
        // Mỗi point armor giảm một lượng damage nhất định
        float armorMultiplier = 0.5f; // 1 point armor giảm 0.5 damage
        float damageTaken = Mathf.Max(1f, incomingDamage - stats.armor * armorMultiplier);

        // Nếu muốn dùng công thức phần trăm:
        // float damageTaken = incomingDamage * 100f / (100f + stats.armor);

        currentHP -= damageTaken;

        Debug.Log(stats.bossName + " took " + damageTaken + " damage. Current HP: " + currentHP);

        if(currentHP <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(stats.bossName + " died! EXP: " + stats.expReward + " | Coins: " + stats.coinReward);
        FindObjectOfType<GameManager>().AddCoin(50); // Thưởng lớn
		
		// Rơi item drop
        if(stats.dropItems != null && stats.dropItems.Length > 0)
        {
            int randomIndex = Random.Range(0, stats.dropItems.Length);
            GameObject drop = Instantiate(stats.dropItems[randomIndex], transform.position, Quaternion.identity) as GameObject;
            // Optional: tự hủy sau X giây
            Destroy(drop, 5f);
        }
		// TODO: Gọi Player nhận EXP/Coins
        Destroy(gameObject);
    }
	
	/// <summary>
    /// Boss tấn công player
    /// </summary>
    public float DealDamage()
    {
        // Đây là sát thương cơ bản
        return stats.damage;
    }
	
	private void OnDrawGizmosSelected()
    {
        // Màu sắc bán kính tấn công
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
