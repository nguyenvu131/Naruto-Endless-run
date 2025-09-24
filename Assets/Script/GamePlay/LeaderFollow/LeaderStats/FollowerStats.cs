using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FollowerStats
{
    public FollowerType type;
    public float moveSpeed = 5f;
    public float attackRange = 3f;
    public float attackCooldown = 1f;
    public int attackDamage = 10;
}
