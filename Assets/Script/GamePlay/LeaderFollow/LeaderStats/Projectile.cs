using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed = 10f;
    public int damage = 5;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Health hp = col.GetComponent<Health>();
            if (hp != null) hp.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
