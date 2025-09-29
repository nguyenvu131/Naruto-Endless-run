using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public GameObject projectilePrefab;
    public Transform firePoint;
    public PlayerStats playerStats; // Lấy từ Player

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Projectile p = proj.GetComponent<Projectile>();
        if (p != null)
        {
            p.Init(playerStats); // Gán PlayerStats cho đạn
        }
    }
}
