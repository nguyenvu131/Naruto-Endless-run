using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FollowerStats
{
    public FollowerType type;
    public float moveSpeed = 5f;
    public float attackRange = 3f;
    public float attackCooldown = 1f;
    public int attackDamage = 10;
	
	public string followerName;
    public int level = 1;
    public float maxHP = 100f;
    public float currentHP = 100f;
    public float damage = 10f;
    public float defense = 5f;
    public float jumpForce = 7f;
	
	// Chỉ số nâng cao
    public float critChance = 0.1f;      // 10%
    public float critMultiplier = 1.5f;  // 150% damage
    public float dodgeChance = 0.05f;    // 5%
	
	public void TakeDamage(float enemyATK)
	{
		// Dodge check
        if (UnityEngine.Random.value < dodgeChance)
        {
            Debug.Log("Dodged!");
            return;
        }

        // Công thức giáp tuyến tính: mỗi point defense giảm 0.5 sát thương
        float defenseMultiplier = 0.5f;
        float damageTaken = Mathf.Max(1f, enemyATK - defense * defenseMultiplier);

        currentHP -= damageTaken;

        Debug.Log("Follow took " + damageTaken + " damage. Current HP: " + currentHP);

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
	}
	
	void Die() {
        Debug.Log("Follow Died!");
    }
}
