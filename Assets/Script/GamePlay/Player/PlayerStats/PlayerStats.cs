using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats {
    // Chỉ số cơ bản
    public int level = 1;
    public float exp = 0;
    public float expToNextLevel = 100;

    public float maxHP = 100;
    public float currentHP = 100;

    public float maxMP = 50;
    public float currentMP = 50;

    public float attack = 100;
    public float defense = 5;

    public float speed = 5f;
    public float stamina = 100;
	public float moveSpeed = 4;
 
    // Chỉ số nâng cao
    public float critChance = 0.1f;      // 10%
    public float critMultiplier = 1.5f;  // 150% damage
    public float dodgeChance = 0.05f;    // 5%

    public int skillPoints = 0;

	//	Gây sát thương
	public float CalculateDamage()
    {
        // Damage cơ bản từ attack
        float dmg = attack;

        // Bonus theo level (ví dụ: mỗi level tăng 5% damage)
        float levelMultiplier = 1f + level * 0.05f;
        dmg *= levelMultiplier;

        // Critical hit check
        if (UnityEngine.Random.value < critChance)
        {
            dmg *= critMultiplier;
            Debug.Log("CRITICAL HIT! Damage: " + dmg);
        }

        // TODO: có thể thêm các buff/debuff sau này, ví dụ: dmg *= BuffMultiplier;

        return dmg;
    }
	
	//	NHẬN SÁT THƯƠNG
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

        Debug.Log("Player took " + damageTaken + " damage. Current HP: " + currentHP);

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    // Hồi máu
    public void Heal(float amount) {
        currentHP = Mathf.Min(maxHP, currentHP + amount);
    }

    // Nhận exp
    public void GainEXP(float amount) {
        exp += amount;
        if (exp >= expToNextLevel) {
            LevelUp();
        }
    }

    // Lên cấp
    void LevelUp() {
        level++;
        exp = 0;
        expToNextLevel = 100 * Mathf.Pow(level, 1.5f);
        skillPoints++;

        maxHP += 20;
        maxMP += 10;
        attack += 5;
        defense += 2;
        speed += 0.2f;

        currentHP = maxHP;
        currentMP = maxMP;

        Debug.Log("Level Up! Now Level " + level);
    }

    void Die()
    {
        Debug.Log("Player Died!");
        
        // Gọi GameManager -> GameOver
    }
}
