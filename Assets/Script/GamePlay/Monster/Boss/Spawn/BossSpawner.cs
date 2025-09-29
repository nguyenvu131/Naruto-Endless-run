using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour {

	public GameObject bossPrefab;
    public Transform spawnPoint;
    public int scoreToSpawn = 200; // Boss xuất hiện khi score >= 200
    private bool bossSpawned = false;

    void Update()
    {
        int currentScore = FindObjectOfType<GameManager>().GetScore();
        if (!bossSpawned && currentScore >= scoreToSpawn)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        bossSpawned = true;
    }
}
