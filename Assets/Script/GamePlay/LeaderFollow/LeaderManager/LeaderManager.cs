using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderManager : MonoBehaviour {

	 public static LeaderManager Instance;

    public Transform player;
    public GameObject leaderPrefab;

    [HideInInspector]
    public List<LeaderFollow> activeLeaders = new List<LeaderFollow>();

    // Spawn leader mới theo player
    public void AddLeader(LeaderData leaderData)
    {
        // Tránh spawn trùng
        foreach (var l in activeLeaders)
        {
            if (l.leaderData == leaderData)
                return;
        }

        Vector3 spawnOffset = new Vector3(-1 * (activeLeaders.Count + 1), 0, 0); // offset để không chồng UI
        GameObject go = Instantiate(leaderPrefab, player.position + spawnOffset, Quaternion.identity);
        LeaderFollow follow = go.GetComponent<LeaderFollow>();
        follow.leaderData = leaderData;
        follow.player = player;

        activeLeaders.Add(follow);
    }
}
