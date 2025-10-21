using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{

    public MonsterStatsSO monsterStatsSO;

    public int baseDamage = 10;
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Monster stats = col.GetComponent<Monster>();
            if (stats != null)
            {
                stats.TakeDamage(baseDamage);
            }
            Destroy(gameObject);
        }
    }
}
