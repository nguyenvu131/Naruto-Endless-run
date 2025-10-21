using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBossStats", menuName = "Stats/Boss Stats")]
public class BossStatsSO : ScriptableObject 
{
	protected BossStats stats;
	[Header("General Info")]
    public string bossName = "New Boss";
    public BossType bossType = BossType.NormalBoss;
    public int bossLevel = 1;

    [Header("Base Stats")]
    public float maxHealth = 1000f;
    public float attackPower = 50f;
    public float defense = 20f;
    public float moveSpeed = 2f;
    public float attackSpeed = 1f;

    [Header("Special Stats")]
    public float critChance = 0.05f;   // 5% tỉ lệ chí mạng
    public float critDamage = 1.5f;   // 150% sát thương khi crit
    public float skillDamageMultiplier = 2f; // x2 cho skill đặc biệt
    public float armorPenetration = 0f; // xuyên giáp

    [Header("Rewards")]
    public int expReward = 500;
    public int goldReward = 200;
    public string[] dropItems; // tên item rớt ra

    [Header("AI Config")]
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public bool canSummonMinions = false;
    public string[] summonMinionIDs; // ID quái có thể triệu hồi

    [Header("Visuals")]
    public Sprite bossIcon;
    public GameObject bossPrefab;

    /// <summary>
    /// Hàm clone để khi runtime dùng bản copy thay vì chỉnh trực tiếp ScriptableObject gốc
    /// </summary>
    public BossStatsSO Clone()
    {
        BossStatsSO clone = ScriptableObject.CreateInstance<BossStatsSO>();
        clone.bossName = bossName;
        clone.bossType = bossType;
        clone.bossLevel = bossLevel;

        clone.maxHealth = maxHealth;
        clone.attackPower = attackPower;
        clone.defense = defense;
        clone.moveSpeed = moveSpeed;
        clone.attackSpeed = attackSpeed;

        clone.critChance = critChance;
        clone.critDamage = critDamage;
        clone.skillDamageMultiplier = skillDamageMultiplier;
        clone.armorPenetration = armorPenetration;

        clone.expReward = expReward;
        clone.goldReward = goldReward;
        clone.dropItems = dropItems;

        clone.detectionRange = detectionRange;
        clone.attackRange = attackRange;
        clone.canSummonMinions = canSummonMinions;
        clone.summonMinionIDs = summonMinionIDs;

        clone.bossIcon = bossIcon;
        clone.bossPrefab = bossPrefab;

        return clone;
    }
}
