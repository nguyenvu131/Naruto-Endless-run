using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float moveSpeed = 3f;
	
	public int maxHealth = 100;
    private int currentHealth;

    public int armor = 50; // giáp của enemy
	
	void Start()
    {
        currentHealth = maxHealth;
    }
	
    void Update()
    {
        // Enemy luôn chạy sang trái (ngược hướng player chạy)
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Nếu đi quá xa khỏi camera thì disable
        if (Camera.main != null)
        {
            if (transform.position.x < Camera.main.transform.position.x - 20f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Player chết khi va chạm
            collision.collider.GetComponent<PlayerController>().SendMessage("Die");
            gameObject.SetActive(false);
        }
    }
	
	public void TakeDamage(int rawDamage)
    {
        int finalDamage = ArmorFormula.CalculateDamage(rawDamage, armor);

        currentHealth -= finalDamage;
        Debug.Log(name + " nhận " + finalDamage + " damage (sau giáp), máu còn: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " đã chết!");
        Destroy(gameObject);
    }
}
