using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage = 10;
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            MonsterStats stats = col.GetComponent<MonsterStats>();
            if (stats != null)
            {
                stats.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
