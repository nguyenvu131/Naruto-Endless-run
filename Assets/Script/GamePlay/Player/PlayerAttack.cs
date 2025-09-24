using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public PlayerStats playerStats;      // Kết nối PlayerStats
    public float attackRate = 1f;        // Số lần tấn công / giây
    private float nextAttackTime = 0f;

    public float attackRange = 3f;       // Khoảng cách tấn công
    public LayerMask monsterLayer;       // Layer của quái

    void Update()
    {
        // Auto Attack theo thời gian
        if (Time.time >= nextAttackTime)
        {
            AutoAttack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void AutoAttack()
    {
        // Tìm tất cả quái trong phạm vi attackRange
        Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(transform.position, attackRange, monsterLayer);

        foreach (Collider2D hit in hitMonsters)
        {
            MonsterStats monster = hit.GetComponent<MonsterComponent>().monsterStats; // MonsterComponent chứa MonsterStats

            if (monster != null)
            {
                float dmg = playerStats.CalculateDamage();   // Tính damage player
                monster.TakeDamage(dmg);                     // Gây sát thương cho quái
            }
        }
    }
	
	
    // Vẽ debug range attack trong Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
