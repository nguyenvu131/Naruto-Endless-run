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
    }

    void Update()
    {
        if (statsSo == null) return;

        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + statsSo.attackSpeed;
            Attack();
        }
    }

    void Attack()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;

            // Set damage từ BossStatsSO
            BossBullet bb = bullet.GetComponent<BossBullet>();
            if (bb != null)
            {
                bb.damage = statsSo.attackPower;
            }
        }
    }

    /// <summary>
    /// Boss nhận sát thương từ player
    /// </summary>
    public void TakeDamage(float incomingDamage)
    {
        if (statsSo == null) return;

        // Công thức giáp tuyến tính
        float armorMultiplier = 0.5f; // 1 point defense giảm 0.5 damage
        float damageTaken = Mathf.Max(1f, incomingDamage - statsSo.defense * armorMultiplier);

        currentHP -= damageTaken;

        Debug.Log(statsSo.bossName + " took " + damageTaken + " damage. Current HP: " + currentHP);

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(statsSo.bossName + " died! EXP: " + statsSo.expReward + " | Gold: " + statsSo.goldReward);

        // Add vàng cho Player/Manager
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.AddCoin(statsSo.goldReward);
        }

        // Rơi item drop
        if (statsSo.dropItems != null && statsSo.dropItems.Length > 0)
        {
            int randomIndex = Random.Range(0, statsSo.dropItems.Length);
            string itemName = statsSo.dropItems[randomIndex];

            // Ở đây mình giả sử bạn có hệ thống ItemSpawner hoặc prefab theo tên
            Debug.Log("Drop item: " + itemName);
        }

        // UI Victory
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowVictoryPanel();
        }

        // Sau 3 giây thì chuyển scene
        Invoke("GoToNextScene", 3f);

        Destroy(gameObject);
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    /// <summary>
    /// Boss tấn công player → return damage
    /// </summary>
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
