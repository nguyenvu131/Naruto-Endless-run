using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFireBreath : Skill
{
    public GameObject firePrefab;
    public Transform fireSpawn;

    protected override void Activate()
    {
        Debug.Log("Phun Lửa!");
        Instantiate(firePrefab, fireSpawn.position, Quaternion.identity);
    }
}
