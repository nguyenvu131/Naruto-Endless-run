using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {

	public float speed = 10f;            // Tốc độ bay của đạn
    public float lifeTime = 3f;          // Thời gian tồn tại của đạn
    public PlayerStats playerStats;      // Kết nối PlayerStats để lấy damage
	
	// Thêm 2 layer để bullet biết target
    public LayerMask enemyLayer;
    public LayerMask bossLayer;
	
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Đẩy đạn về hướng hiện tại của transform
        rb.velocity = transform.right * speed;

        // Hủy đạn sau lifeTime giây
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Va chạm quái
        if (collision.CompareTag("Enemy"))
        {
            // Lấy component EnemyStats
            Monster enemy = collision.GetComponent<Monster>();
            if (enemy != null)
            {
                float dmg = playerStats.CalculateDamage();
                enemy.TakeDamage(dmg);
                Debug.Log("Enemy nhan! Damage: " + dmg);
            }
            Destroy(gameObject); // Hủy đạn
        }

        // Va chạm boss
        if (collision.CompareTag("Boss"))
        {
            Boss boss = collision.GetComponent<Boss>();
            if (boss != null)
            {
                float dmg = playerStats.CalculateDamage();
                boss.TakeDamage(dmg);
                Debug.Log("Hit Boss! Damage: " + dmg);
            }
            Destroy(gameObject); // Hủy đạn
        }

        // Va chạm vật cản khác
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
