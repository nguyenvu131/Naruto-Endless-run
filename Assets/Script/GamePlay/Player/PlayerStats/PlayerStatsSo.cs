using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "stats/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
	public PlayerStats stats;
    [Header("Chỉ số gốc")]
    public int baseLevel = 1;
    public float baseHP = 100f;
    public float baseAttack = 20f;
    public float baseDefense = 5f;
    public float baseSpeed = 5f;

    [Header("Chỉ số đặc biệt")]
    [Range(0f, 1f)] public float critChance = 0.1f;
    public float critMultiplier = 2f;
    [Range(0f, 1f)] public float dodgeChance = 0.05f;

    [Header("Prefab hiệu ứng")]
    public GameObject deathEffectPrefab;

    /// <summary>
    /// Tạo PlayerStats runtime từ ScriptableObject
    /// </summary>
    public PlayerStats CreateRuntimeStats(int level)
    {
        PlayerStats stats = new PlayerStats();

        stats.level = level;
        stats.maxHP = baseHP + (level - 1) * 20f;
        stats.currentHP = stats.maxHP;
        stats.attack = baseAttack + (level - 1) * 5f;
        stats.defense = baseDefense + (level - 1) * 2f;
        stats.speed = baseSpeed + (level - 1) * 0.2f;

        stats.critChance = critChance;
        stats.critMultiplier = critMultiplier;
        stats.dodgeChance = dodgeChance;

        return stats;
    }
}
