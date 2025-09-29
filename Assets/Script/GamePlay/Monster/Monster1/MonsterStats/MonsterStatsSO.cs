using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterStats", menuName = "Stats/Monster Stats")]
public class MonsterStatsSO : ScriptableObject
{
    //public MonsterStats stats;

    public GameObject monsterPrefab; // trong MonsterStatsSO
    
    [Header("Thông tin cơ bản")]
    public MonsterType type = MonsterType.NormalMonster;
    public string monsterName = "Monster";

    [Header("Chỉ số gốc (base stats)")]
    public float baseHP = 50f;
    public float baseDamage = 10f;
    public float baseSpeed = 2f;

    [Header("Phần thưởng khi giết")]
    public int baseExpReward = 10;
    public int baseCoinReward = 5;

    [Header("Tỉ lệ đặc biệt")]
    [Range(0f, 1f)] public float critChance = 0f;
    public float critMultiplier = 1.5f;
    [Range(0f, 1f)] public float dodgeChance = 0f;

    [Header("Prefab hiệu ứng")]
    public GameObject deathEffectPrefab;

    /// <summary>
    /// Tạo một bản sao MonsterStats runtime từ ScriptableObject gốc
    /// </summary>
    public MonsterStats CreateRuntimeStats(int level)
    {
        MonsterStats stats = new MonsterStats();
        stats.monsterName = monsterName;
        stats.type = type;
        stats.baseHP = baseHP;
        stats.baseDamage = baseDamage;
        stats.baseSpeed = baseSpeed;
        stats.expReward = baseExpReward;
        stats.coinReward = baseCoinReward;
        stats.critChance = critChance;
        stats.critMultiplier = critMultiplier;
        stats.dodgeChance = dodgeChance;
        stats.deathEffectPrefab = deathEffectPrefab;

        // Gọi InitStats để scale theo level + type
        stats.InitStats(type, level);

        return stats;
    }
}
