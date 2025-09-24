using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoShooter : MonoBehaviour {

	public GameObject bulletPrefab;     // Prefab đạn
    public Transform firePoint;         // Vị trí bắn
    public float fireRate = 1f;         // Tốc độ bắn (số phát/giây)
    public float attackRange = 5f;      // Phạm vi tấn công
    public LayerMask enemyLayer;        // Layer của Enemy

    private float fireCooldown = 0f;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        // Kiểm tra có quái trong phạm vi
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, attackRange, enemyLayer);
        if (enemy != null)
        {
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate;
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

        Debug.Log("Player Auto Shoot!");
    }

    // Vẽ phạm vi attack trong Scene để debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
