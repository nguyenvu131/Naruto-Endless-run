using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public string itemName;
    public ShopItemType itemType;
    public int price;
    public bool isPurchased;
	
    public Sprite icon;
    public LeaderData leaderData;       // Nếu là leader
    public FollowerStats followerData;  // Nếu là follower
	
    public float bonusHP;
    public float bonusATK;
    public float bonusDEF;
    public float bonusSpeed;
}
