using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterComponent : MonoBehaviour {

	public MonsterStats monsterStats;

    void Start()
    {
        if(monsterStats != null)
        {
            monsterStats.InitStats(monsterStats.type, monsterStats.level);
        }
    }
}
