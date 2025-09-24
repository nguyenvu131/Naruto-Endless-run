using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderManager : MonoBehaviour {

	public PlayerController player;
    public GameObject leaderPrefab;
    public int leaderCount = 3;

    private List<GameObject> leaders = new List<GameObject>();

    void Start()
    {
        SpawnLeaders();
    }

    void SpawnLeaders()
    {
        Transform lastTarget = player.transform;

        for (int i = 0; i < leaderCount; i++)
        {
            GameObject leader = Instantiate(leaderPrefab, lastTarget.position - new Vector3(1.5f * (i+1), 0, 0), Quaternion.identity);
            LeaderFollow lf = leader.GetComponent<LeaderFollow>();
            lf.target = lastTarget; // follow player hoặc leader trước đó
            leaders.Add(leader);

            lastTarget = leader.transform;
        }
    }
}
