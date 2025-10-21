using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterComponent : MonoBehaviour {

	public MonsterStats monsterStats;
	
	void Awake()
    {
        if(monsterStats == null)
            monsterStats = new MonsterStats();
    }
	
    void Start()
    {
        if(monsterStats != null)
        {
            monsterStats.InitStats(monsterStats.type, monsterStats.level);
        }
    }
}
