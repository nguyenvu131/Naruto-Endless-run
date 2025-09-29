using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed = 10f;

    private PlayerStats playerStats; // Tham chiếu PlayerStats

    // Hàm để gán PlayerStats khi tạo đạn
    public void Init(PlayerStats stats)
    {
        playerStats = stats;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Monster hp = col.GetComponent<Monster>();
            if (hp != null && playerStats != null)
            {
                float dmg = playerStats.CalculateDamage(); // damage tính theo Player
                hp.TakeDamage(dmg);
                Debug.Log("Enemy trúng đạn, nhận " + dmg + " damage từ Player!");
            }

            Destroy(gameObject);
        }
    }
}
