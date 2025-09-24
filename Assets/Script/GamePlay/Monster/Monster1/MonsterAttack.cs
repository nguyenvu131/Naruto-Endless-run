using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour {

	public MonsterStats stats;          // Tham chiếu stats của monster
    public float attackCooldown = 2f;   // Thời gian giữa các lần tấn công
    private float lastAttackTime = 0f;
	
	public float attackRange = 2f; // phạm vi tấn công
    public PlayerStats playerStats;     // Tham chiếu player stats

    void Start()
    {
        if (stats == null)
        {
            stats = GetComponent<MonsterStats>();
        }
    }

    void Update()
    {
        if (playerStats == null)
            return;

        // Kiểm tra khoảng cách tấn công nếu cần
        float distance = Vector3.Distance(transform.position, playerStatsPosition());
        if (distance <= 2f) // khoảng cách tấn công
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    void AttackPlayer()
    {
        if (stats == null || playerStats == null)
            return;

        // Tính sát thương monster gây ra
        float damage = stats.CalculateDamage();

        // Gọi Player nhận sát thương
        playerStats.TakeDamage(damage);

        Debug.Log(stats.monsterName + " attacked Player for " + damage + " damage!");
    }

    // Hàm giả lập vị trí player
    Vector3 playerStatsPosition()
    {
        // Trong prototype, giả lập tại origin
        return Vector3.zero;
    }
	
	// ===================== Gizmos =====================
    private void OnDrawGizmosSelected()
    {
        // Màu sắc bán kính tấn công
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
