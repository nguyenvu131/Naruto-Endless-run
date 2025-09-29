using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float damage = 10f;
    public float speed = 5f;
    public float lifeTime = 5f;

    [Header("Effects")]
    public GameObject hitEffect; // particle effect khi trúng player

    [Header("Audio")]
    public AudioClip shootSound;   // âm thanh khi bắn
    public AudioClip hitSound;     // âm thanh khi va chạm
    private AudioSource audioSource;

    private Vector2 direction;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Khởi tạo bullet
    public void Init(float dmg, Vector2 dir)
    {
        damage = dmg;
        direction = dir.normalized;
        Destroy(gameObject, lifeTime);

        // Phát âm thanh bắn
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // Phát âm thanh va chạm
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }

            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }

            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
