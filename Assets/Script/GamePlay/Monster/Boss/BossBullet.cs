using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

	public float speed = 5f;      // Tốc độ đạn
    public float lifeTime = 5f;   // Tự hủy sau X giây
    public float damage = 50f;    // Damage gây ra

    public bool moveLeft = true;  // True: di chuyển sang trái, false: sang phải

    void Start()
    {
        // Tự hủy sau lifeTime
        Destroy(gameObject, lifeTime);

        // Flip sprite nếu cần
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr != null)
        {
            if(moveLeft && transform.localScale.x > 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            else if(!moveLeft && transform.localScale.x < 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void Update()
    {
        // Di chuyển đạn
        if(moveLeft)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        else
            transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

		if (other.CompareTag("Player"))
        {
            Player stats = other.GetComponent<Player>();
            if (stats != null)
            {
                stats.TakeDamage(damage);
                Debug.Log("Player nhan! Damage: " + damage);
            }
            Destroy(gameObject);
        }
		else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
