using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoAttack : MonoBehaviour {

	public float attackRange = 2f;           // Phạm vi tấn công
    public float attackCooldown = 1f;        // Thời gian giữa 2 đòn đánh
    private float lastAttackTime = 0f;

    public LayerMask enemyLayer;             // Layer của quái
    public Transform attackPoint;            // Điểm xuất phát đòn đánh

    public GameObject projectilePrefab;      // Nếu tấn công tầm xa
    public float projectileSpeed = 10f;

    void Update()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Collider2D target = FindNearestEnemy();
            if (target != null)
            {
                Attack(target);
                lastAttackTime = Time.time;
            }
        }
    }

    Collider2D FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        if (hits.Length == 0) return null;

        // Chọn quái gần nhất
        Collider2D nearest = hits[0];
        float minDist = Vector2.Distance(transform.position, nearest.transform.position);
        for (int i = 1; i < hits.Length; i++)
        {
            float dist = Vector2.Distance(transform.position, hits[i].transform.position);
            if (dist < minDist)
            {
                nearest = hits[i];
                minDist = dist;
            }
        }
        return nearest;
    }

    void Attack(Collider2D enemy)
    {
        // Nếu cận chiến
        enemy.GetComponent<EnemyHealth>().TakeDamage(PlayerStatsManager.Instance.GetDamage());

        // Nếu tầm xa
        if(projectilePrefab != null)
        {
            GameObject proj = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
            Vector2 direction = (enemy.transform.position - attackPoint.position).normalized;
            proj.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        }

        // Thêm animation và hiệu ứng tại đây
        // anim.SetTrigger("Attack");
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
