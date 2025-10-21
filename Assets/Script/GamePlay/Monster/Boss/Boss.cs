using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{

    [Header("Data SO")]
    public BossStatsSO statsSo;
    public BossStats stats;

    private float currentHP;

    [Header("Attack")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float nextAttack;

    [Header("Target")]
    public Transform player; // Target để auto attack

    void Start()
    {
        if (statsSo != null)
        {
            currentHP = statsSo.maxHealth;
        }
        else
        {
            Debug.LogError("BossStatsSO chưa được gán!");
            currentHP = 100f; // fallback
        }

        // Nếu chưa gán player thì tìm tự động
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (statsSo == null || player == null) return;

        // Auto attack nếu player trong range
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= statsSo.attackRange && Time.time > nextAttack)
        {
            nextAttack = Time.time + statsSo.attackSpeed;
            Attack();
        }
    }

    void Attack()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Set damage từ BossStatsSO
            BossBullet bb = bullet.GetComponent<BossBullet>();
            if (bb != null)
            {
                bb.damage = statsSo.attackPower;

                // Hướng về player
                if (player != null)
                {
                    Vector3 dir = (player.position - firePoint.position).normalized;
                    bb.SetDirection(dir);
                }
            }
        }
    }

    public void TakeDamage(float incomingDamage)
    {
        if (statsSo == null) return;

        float armorMultiplier = 0.5f;
        float damageTaken = Mathf.Max(1f, incomingDamage - statsSo.defense * armorMultiplier);

        currentHP -= damageTaken;
        if (currentHP < 0)
            currentHP = 0;

        Debug.Log(statsSo.bossName + " took " + damageTaken + " damage. Current HP: " + currentHP);

        if (currentHP == 0f)
            Die();
    }

    void Die()
    {
        Debug.Log(statsSo.bossName + " died! EXP: " + statsSo.expReward + " | Gold: " + statsSo.goldReward);

        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
            gm.AddCoin(statsSo.goldReward);

        if (statsSo.dropItems != null && statsSo.dropItems.Length > 0)
        {
            int randomIndex = Random.Range(0, statsSo.dropItems.Length);
            string itemName = statsSo.dropItems[randomIndex];
            Debug.Log("Drop item: " + itemName);
        }

        if (UIManager.instance != null)
            UIManager.instance.ShowVictoryPanel();

        Invoke("GoToNextScene", 3f);
        Destroy(gameObject);
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public float DealDamage()
    {
        return statsSo != null ? statsSo.attackPower : 10f;
    }

    private void OnDrawGizmosSelected()
    {
        if (statsSo == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, statsSo.attackRange);
    }

    public float GetCurrentHP()
    {
        return currentHP;
    }
}
