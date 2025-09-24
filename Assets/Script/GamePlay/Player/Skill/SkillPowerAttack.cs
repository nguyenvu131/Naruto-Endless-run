using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPowerAttack : Skill
{
    public float attackRange = 3f;
    public LayerMask enemyLayer;

    protected override void Activate()
    {
        Debug.Log("Gậy Sắt Quét!");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            Destroy(enemy.gameObject); // tiêu diệt enemy
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
