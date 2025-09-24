using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerAI : MonoBehaviour {

	public Transform leader;            // Leader để follow
    public FollowerStats stats;         // Chỉ số follower
    public float followDistance = 2f;   // Khoảng cách so với leader
    public LayerMask enemyLayer;

    private float lastAttackTime;

    void Update()
    {
        FollowLeader();
        DetectAndAttackEnemy();
    }

    void FollowLeader()
    {
        if (leader == null) return;

        // Vị trí mục tiêu là sau leader followDistance
        Vector3 targetPos = leader.position - leader.right * followDistance;

        // Di chuyển mượt đến vị trí follow
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * stats.moveSpeed);
    }

    void DetectAndAttackEnemy()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, stats.attackRange, enemyLayer);
        if (enemy != null && Time.time > lastAttackTime + stats.attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack(enemy.gameObject);
        }
    }

    void Attack(GameObject enemy)
    {
        Debug.Log(stats.type + " attack " + enemy.name);

        // Giả sử enemy có script Health
        Health hp = enemy.GetComponent<Health>();
        if (hp != null)
        {
            hp.TakeDamage(stats.attackDamage);
        }

        // Tuỳ loại follower có thể có hiệu ứng khác
        switch (stats.type)
        {
            case FollowerType.Sakura:
                // Warrior đánh cận chiến
                break;
            case FollowerType.Sasuke:
                // Archer bắn projectile
                break;
            case FollowerType.Kakashi:
                // Mage tạo AOE hoặc buff Leader
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }
}
