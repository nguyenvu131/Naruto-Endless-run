using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonsterBase
{
	[Header("Data References")]
    public MonsterStatsSO statsSO;   // Dữ liệu gốc từ ScriptableObject
    public HealthBarUI healthBar;    // UI máu quái

    [Header("Level Settings")]
    public int level = 1;

    protected override void Start()
    {
        base.Start();

        // Nếu có ScriptableObject -> tạo stats runtime
        if (statsSO != null)
        {
            stats = statsSO.CreateRuntimeStats(level);
            stats.InitStats(stats.type, stats.level);

            currentHP = (int)stats.currentHP;
            maxHP = (int)stats.maxHP;
        }

        // Gắn thanh máu nếu có
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(stats.maxHP);
            healthBar.SetHealth(stats.currentHP);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (stats == null || isDead) return;

        // Không cho máu âm
        if (stats.currentHP < 0)
            stats.currentHP = 0;

        // Cập nhật UI máu
        if (healthBar != null)
            healthBar.SetHealth(stats.currentHP);

        // Chết
        if (stats.currentHP <= 0)
            Die();
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg); // Gọi hàm cha xử lý logic chung

        if (healthBar != null)
            healthBar.SetHealth(stats.currentHP);
    }

    protected override void Die()
    {
        base.Die(); // Gọi logic chết từ cha
        Debug.Log("Monster {name} died!");
    }
}