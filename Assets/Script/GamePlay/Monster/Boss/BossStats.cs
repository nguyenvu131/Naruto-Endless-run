using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBossStats", menuName = "Game/Boss Stats")]
public class BossStats : ScriptableObject
{
    [Header("Basic Info")]
    public string bossName = "Shadow Ninja";
    public int level = 1;
    public string description = "Boss cuối màn, khó nhằn";

    [Header("Core Stats")]
    public int maxHP = 5000;
    public int armor = 50;         // Giảm sát thương
    public int damage = 100;       // Damage cận chiến hoặc skill cơ bản
    public float moveSpeed = 3f;
    public float attackCooldown = 2f;
    public float skillCooldown = 5f;

    [Header("Rewards")]
    public int expReward = 1000;   // Exp khi hạ boss
    public int coinReward = 500;   // Coins khi hạ boss

    [Header("Drops")]
    public GameObject[] dropItems; // Item random drop

    [Header("Skill Settings")]
    public string[] skillNames;    // Tên skill
    public int[] skillDamages;     // Damage từng skill
    public float[] skillCooldowns; // Thời gian cooldown từng skill

    [Header("Other Settings")]
    public float detectionRange = 10f; // Khoảng cách phát hiện player
    public float enrageThreshold = 0.5f; // %HP để vào trạng thái Enrage
}
