using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossAI : MonoBehaviour
{
    public BossStatsSO stats; // Gán ScriptableObject vào đây

    private float currentHP;
    private Rigidbody2D rb;
    private Animator anim;
    protected Transform player;

    private float lastAttackTime;
    private bool isEnraged = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (stats != null)
        {
            currentHP = stats.maxHealth;
        }
        else
        {
            Debug.LogError("BossStatsSO chưa được gán!");
            currentHP = 100f;
        }

        // player = PlayerController.Instance.transform; // tùy hệ thống Player
    }

    void Update()
    {
        if (stats == null || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= stats.detectionRange)
        {
            // Di chuyển lại gần player
            MoveTowardsPlayer();

            // Check Enrage (nổi điên khi còn ít máu)
            if (!isEnraged && currentHP <= stats.maxHealth * 0.3f) // 30% máu
            {
                EnterEnrage();
            }

            // Attack cơ bản
            if (Time.time > lastAttackTime + stats.attackSpeed)
            {
                BasicAttack();
                lastAttackTime = Time.time;
            }

            // TODO: Tích hợp hệ thống Skill nếu có
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(dir.x * stats.moveSpeed, rb.velocity.y);

        if (anim != null)
            anim.SetBool("Run", true);
    }

    void BasicAttack()
    {
        Debug.Log(stats.bossName + " tấn công cơ bản gây " + stats.attackPower + " sát thương!");
        if (anim != null) anim.SetTrigger("Attack");

        // TODO: PlayerController.Instance.TakeDamage(stats.attackPower);
    }

    public void TakeDamage(float dmg)
    {
        float armorMultiplier = 0.5f; // 1 point defense = giảm 0.5 damage
        float finalDamage = Mathf.Max(1f, dmg - stats.defense * armorMultiplier);

        currentHP -= finalDamage;

        Debug.Log(stats.bossName + " nhận " + finalDamage + " damage. Còn " + currentHP + " HP");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void EnterEnrage()
    {
        isEnraged = true;
        stats.attackPower *= 1.5f;
        stats.moveSpeed *= 1.5f;
        Debug.Log(stats.bossName + " đã nổi điên! Damage và Speed tăng mạnh!");
    }

    void Die()
    {
        Debug.Log(stats.bossName + " đã bị tiêu diệt!");
        if (anim != null) anim.SetTrigger("Die");

        // Thưởng cho Player
        Debug.Log("Nhận " + stats.expReward + " EXP và " + stats.goldReward + " Gold");

        // Drop Item (nếu có prefab drop sẵn)
        if (stats.dropItems != null && stats.dropItems.Length > 0)
        {
            int rand = Random.Range(0, stats.dropItems.Length);
            Debug.Log("Drop item: " + stats.dropItems[rand]);
        }

        Destroy(gameObject, 2f);
    }

    public float GetCurrentHP()
    {
        return currentHP;
    }
}