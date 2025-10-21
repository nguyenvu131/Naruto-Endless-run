using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterStats {
    public MonsterType type = MonsterType.NormalMonster;

	public string monsterName = "Monster";
    public float baseHP = 50f;
    public float baseDamage = 10f;
    public float baseSpeed = 2f;
	
    public int level = 1;

    public float maxHP = 50;
    public float currentHP = 50;

    public float attack = 5;
    public float defense = 2;
    public float speed = 2f;

    public float critChance = 0f;
    public float critMultiplier = 1.5f;
    public float dodgeChance = 0f;

    public int expReward = 10;
    public int coinReward = 5;
	
	public float currentDamage = 10;
	public float currentSpeed = 5; 
	
	// Particle effect khi quái chết
    public GameObject deathEffectPrefab;
    

	
    // Khởi tạo stats theo loại + level
    public void InitStats(MonsterType monsterType, int lvl)
    {
        type = monsterType;
        level = lvl;

        // Base stats
        maxHP = 50 * (1 + level * 0.2f);
        attack = 5 * (1 + level * 0.15f);
        defense = 2 + level * 1.2f;
        speed = 2f + level * 0.05f;

        expReward = 10 * level;
        coinReward = 5 * level;

        // Elite scaling
        if (type == MonsterType.Elite)
        {
            maxHP *= 1.5f;
            attack *= 1.3f;
            defense *= 1.2f;
            critChance = 0.1f;   // 10%
            dodgeChance = 0.05f; // 5%
        }
        // Boss Mini scaling
        else if (type == MonsterType.BossMini)
        {
            maxHP *= 3f;
            attack *= 2f;
            defense *= 1.3f;
            critChance = 0.15f;
            dodgeChance = 0.1f;
        }
        // Boss scaling
        else if (type == MonsterType.Boss)
        {
            maxHP *= 10f;
            attack *= 5f;
            defense *= 1.5f;
            critChance = 0.2f;
            dodgeChance = 0.15f;
        }

        currentHP = maxHP;
    }

    // Tính damage gây ra
    public float CalculateDamage() {
        float dmg = attack;
        if (Random.value < critChance) {
            dmg *= critMultiplier;
            Debug.Log(type + " Critical Hit!");
        }
        return dmg;
    }

    // Nhận sát thương
    // public void TakeDamage(float playerATK) {
        // if (Random.value < dodgeChance) {
            // Debug.Log(type + " Dodged!");
            // return;
        // }

        // float damageTaken = Mathf.Max(1, playerATK - defense * 0.5f);
        // currentHP -= damageTaken;

        // if (currentHP <= 0) {
            // Die();
        // }
    // }
	
	// Nhận sát thương với công thức giáp scale theo level + type
    public void TakeDamage(float incomingDamage)
    {
        // Dodge check
        if (UnityEngine.Random.value < dodgeChance)
        {
            Debug.Log(monsterName + " Dodged the attack!");
            return;
        }

        // Giáp tuyến tính scale theo level + type
        float defenseMultiplier = 0.5f; // mỗi point defense giảm 0.5 damage
        float levelMultiplier = 1f + level * 0.1f;
        float typeMultiplier = 1f;

        switch(type)
        {
            case MonsterType.Elite: typeMultiplier = 1.2f; break;
            case MonsterType.BossMini: typeMultiplier = 1.5f; break;
            case MonsterType.Boss: typeMultiplier = 2f; break;
        }

        float damageTaken = Mathf.Max(1f, incomingDamage - defense * defenseMultiplier * levelMultiplier * typeMultiplier);

        currentHP -= damageTaken;

        Debug.Log(monsterName + " took " + damageTaken + " damage. Current HP: " + currentHP);

        if (currentHP < 0)
			currentHP = 0;

		if (currentHP == 0)
		{
			Die();
		}
    }

    void Die() {

        Debug.Log(type + " died! EXP: " + expReward + " | Coins: " + coinReward);
		// Nhan coin
		PlayerStatsManager.Instance.PlayerGainEXP(expReward);
		
		// Hiển thị hiệu ứng particle khi quái chết
        if(deathEffectPrefab != null)
        {
            GameObject effect = GameObject.Instantiate(deathEffectPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            // Optional: di chuyển effect về vị trí quái
            effect.transform.position = Vector3.zero; // thay bằng vị trí monster nếu có Transform
            GameObject.Destroy(effect, 2f); // tự hủy sau 2 giây
        }

        // Gọi PlayerStatsManager.Instance.PlayerGainEXP(expReward);
        // Gọi GameManager.Instance.AddCoins(coinReward);
    }
	
	public void InitStats(int level, float hpMultiplier, float damageMultiplier, float speedMultiplier)
    {
        this.level = level;

        currentHP = baseHP + (level * hpMultiplier);
        currentDamage = baseDamage + (level * damageMultiplier);
        currentSpeed = baseSpeed + (level * 0.1f * speedMultiplier);
    }
}
