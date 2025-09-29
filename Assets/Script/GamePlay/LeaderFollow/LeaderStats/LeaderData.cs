using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLeaderData", menuName = "Game/LeaderData")]
public class LeaderData : ScriptableObject
{
    public FollowerStats followerStats;

    [Header("Basic Info")]
    public string leaderName;
    public int level;
    public Sprite icon; // avatar hoặc hình đại diện
	
	[Header("Stats cơ bản")]
    public int baseHP = 100;
    public int baseAttack = 20;
    public int baseDefense = 5;

    [Header("Stats")]
    public float maxHP;
    public float attack;
    public float defense;
    public float moveSpeed;
    public float jumpForce;

    [Header("Skills & Buffs")]
    public LeaderSkillData[] skills;      // có thể dùng ScriptableObject riêng cho skill
    public LeaderBuffData[] buffs;        // có thể dùng ScriptableObject riêng cho buff

    [Header("Visuals")]
    public string title;            // hiển thị trên đầu leader
    public Color titleColor = Color.white;
	
	// Thêm dòng này để theo dõi đã mua hay chưa
    public bool isPurchased = false;
	public int price;

}
