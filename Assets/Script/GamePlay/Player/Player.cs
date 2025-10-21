using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
 {
    private static PlayerStats Instance;   // Singleton nếu muốn gọi nhanh
    public PlayerStats stats;

    // [Header("Data gốc từ ScriptableObject")]
    // public PlayerStatsSO statsDataSO;

    [Header("Level hiện tại")]
    public int level = 1;
    // [Header("stats (HP, ATK...)")]

	// public float maxHP = 100f;
    // private float currentHP;

    public HealthBarUI healthBar; // Gắn script UI vào đây

    void Start()
    {
        stats.currentHP = stats.maxHP;
        
        // if (statsDataSO != null)
        // {
            // stats = statsDataSO.CreateRuntimeStats(level);
        // }
        // else
        // {
            // Debug.LogError("PlayerStatsSo chưa được gán!");
        // }
    }

    public void TakeDamage(float dmg)
    {
        if (stats != null)
            stats.TakeDamage(dmg);

         // Không cho máu bị âm
        if (stats.currentHP < 0)
            stats.currentHP = 0;

        if (stats.currentHP == 0)
        {
            Die();
        }
    }

     void Die()
    {
        Debug.Log("Player Died!");
        FindObjectOfType<GameManager>().GameOver();
        // Gọi GameManager -> GameOver
    }

    public float GetAttackDamage()
    {
        return stats != null ? stats.CalculateDamage() : 0f;
    }
}
