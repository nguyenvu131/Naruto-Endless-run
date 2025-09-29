using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	public MonsterStatsSO statsSO;
	public MonsterStats stats;
	public int level = 1;
	 public HealthBarUI healthBar; // Gắn script UI vào đây

	// Use this for initialization
	void Start()
    {
		//stats = statsSO.CreateRuntimeStats(level);

    }
	
	// Update is called once per frame
	void Update () {
		if (stats.currentHP <= 0) {
            Die();
        }
	}

	public void TakeDamage(float dmg)
	{
		stats.TakeDamage(dmg);
	}

	void Die()
	{
		Debug.Log("Monster died!");
		Destroy(gameObject);  // Hủy quái tại đây
		
        // Gọi GameManager.GameOver();
	}
	

	
	
}
