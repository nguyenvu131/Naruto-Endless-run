using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossStats {
    public string bossName = "Boss";
    public BossType type = BossType.NormalBoss;

    public int level = 1;
    public float maxHP = 500;
    public float currentHP = 500;

    public float attack = 20;
    public float defense = 10;
    public float moveSpeed = 2f;

    public bool isDead = false;
}
