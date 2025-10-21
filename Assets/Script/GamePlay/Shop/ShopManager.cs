using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Data")]
    public ShopItem[] shopItems;   // Danh sách item trong shop
    public LeaderData[] leaders;   // Danh sách leader có thể mua

    [Header("Leader Spawn")]
    public Transform player;          // Player để leader theo sau
    public GameObject leaderPrefab;   // Prefab có script LeaderFollow
    public float leaderOffsetX = -1f; // Khoảng cách offset giữa các leader

    private int leaderCount = 0;      // Đếm số leader đã spawn
	
	protected PlayerStats playerStats;
	
    // ======================
    // MUA ITEM
    // ======================
    public bool BuyItem(int index)
    {
        if (index < 0 || index >= shopItems.Length) return false;

        ShopItem item = shopItems[index];

        if (!item.isPurchased && ProgressionManager.Instance.SpendCoins(item.price))
        {
            item.isPurchased = true;
            Debug.Log("Đã mua item: " + item.itemName);

            // ✅ Cộng chỉ số vào Player
            ApplyItemBonus(item);

            return true;
        }
        else
        {
            Debug.Log("Không đủ coin hoặc đã mua item trước đó!");
            return false;
        }
		
    }

    // ======================
    // MUA LEADER
    // ======================
    public bool BuyLeader(int index)
    {
        if (index < 0 || index >= leaders.Length) return false;

        LeaderData leader = leaders[index];

        if (!leader.isPurchased && ProgressionManager.Instance.SpendCoins(leader.price))
        {
            leader.isPurchased = true;

            // Spawn leader theo player
            SpawnLeader(leader);

            Debug.Log("Đã mua leader: " + leader.leaderName);
            return true;
        }
        else
        {
            Debug.Log("Không đủ coin hoặc đã mua leader trước đó!");
            return false;
        }

    }

    // ======================
    // SPAWN LEADER
    // ======================
    void SpawnLeader(LeaderData leader)
    {
        if (player == null || leaderPrefab == null)
        {
            Debug.LogWarning("Player hoặc LeaderPrefab chưa gán trong ShopManager!");
            return;
        }

        // Tính vị trí spawn dựa trên số lượng leader đã spawn
        Vector3 spawnPos = player.position + new Vector3(leaderOffsetX * (leaderCount + 1), 0, 0);
        GameObject go = Instantiate(leaderPrefab, spawnPos, Quaternion.identity);

        // Gán dữ liệu cho LeaderFollow
        LeaderFollow follow = go.GetComponent<LeaderFollow>();
        if (follow != null)
        {
            follow.leaderData = leader;
            follow.player = player;
            follow.followIndex = leaderCount; // Index để xác định vị trí theo sau
        }

        leaderCount++;
    }
	
	void ApplyItemBonus(ShopItem item)
    {
        if (playerStats == null) return;

        playerStats.maxHP += item.bonusHP;
        playerStats.attack += item.bonusATK;
        playerStats.defense += item.bonusDEF;
        playerStats.moveSpeed += item.bonusSpeed;

        playerStats.currentHP = playerStats.maxHP; // hồi máu full khi nâng cấp

        Debug.Log("Cộng tiềm năng cho Player: " +
                  "+HP {item.bonusHP}, +ATK {item.bonusATK}, +DEF {item.bonusDEF}, +Speed {item.bonusSpeed}");
    }
}
