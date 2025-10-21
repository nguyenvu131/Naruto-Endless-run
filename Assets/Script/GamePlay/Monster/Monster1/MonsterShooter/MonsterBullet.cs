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

    private Transform player;
    private Vector2 moveDir;
    

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        // tìm player theo tag
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            player = target.transform;
            moveDir = (player.position - transform.position).normalized;
        }
        else
        {
            moveDir = transform.right; // nếu không tìm thấy thì bay thẳng theo hướng object
        }

        // tự hủy sau lifeTime giây
        Destroy(gameObject, lifeTime);

        // âm thanh bắn
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    void Update()
    {
        // Di chuyển bằng transform
        transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
            }

            PlayHitEffect();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            PlayHitEffect();
            Destroy(gameObject);
        }
    }

    private void PlayHitEffect()
    {
        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, transform.position);

        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);
    }

   
}