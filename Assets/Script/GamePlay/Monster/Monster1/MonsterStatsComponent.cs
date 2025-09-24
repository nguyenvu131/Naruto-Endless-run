using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatsComponent : MonoBehaviour {

	public MonsterStats stats; // Tham chiếu dữ liệu monster

    void Awake()
    {
        if(stats == null)
            stats = new MonsterStats();
    }
}
