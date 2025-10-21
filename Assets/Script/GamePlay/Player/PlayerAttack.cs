using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour 
{    
	public PlayerStats playerStats;
	

    [Header("Ranged Attack")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float fireRange = 5f;
    public LayerMask enemyLayer; // Gom chung Monster + Boss vào 1 layer

    protected float fireCooldown = 0f;
    private GameObject currentBullet; // Lưu viên đạn hiện tại
	protected bool facingRight = true;
    
	void Update()
    {
        fireCooldown -= Time.deltaTime;

        // Nếu vẫn còn viên đạn cũ thì chưa được bắn tiếp
        if (currentBullet != null)
            return;

        // Tìm enemy trong phạm vi
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, fireRange, enemyLayer);
        if (enemy != null && fireCooldown <= 0f)
        {
            Shoot(enemy.transform);
            fireCooldown = 1f / fireRate;
        }
		
		// Kiểm tra hướng di chuyển để xác định quay mặt
        if (Input.GetAxisRaw("Horizontal") > 0)
            facingRight = true;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            facingRight = false;
    }

    void Shoot(Transform target)
    {
        if (bulletPrefab == null || firePoint == null || target == null)
            return;

        currentBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity) as GameObject;

        Rigidbody2D rb = currentBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {

            Vector2 dir = (target.position - firePoint.position).normalized;
            rb.velocity = dir * 10f; // tốc độ đạn
        }

        BulletPlayer bulletComp = currentBullet.GetComponent<BulletPlayer>();
        if (bulletComp != null)
        {
            bulletComp.playerStats = playerStats;
            bulletComp.enemyLayer = enemyLayer;
            bulletComp.ownerAttack = this; // Gắn lại tham chiếu để callback thủ công
        }

        Debug.Log("Player Auto Shoot!");
    }

    // Hàm callback gọi khi đạn bị hủy
    public void OnBulletDestroyed(BulletPlayer bullet)
	{
		currentBullet = null;
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fireRange);
    }
}
