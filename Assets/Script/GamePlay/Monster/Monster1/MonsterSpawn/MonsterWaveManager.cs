using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaveManager : MonoBehaviour {
    public static MonsterWaveManager Instance;

    [Header("Monster Prefabs")]
    public GameObject normalPrefab;
    public GameObject elitePrefab;
    public GameObject bossMiniPrefab;
    public GameObject bossPrefab;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints; // danh sách vị trí spawn
    public int monstersPerWave = 5;

    private List<MonsterStats> aliveMonsters = new List<MonsterStats>();
    private int currentWave = 0;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Gọi để bắt đầu 1 wave mới
    public void StartWave(int waveNumber) {
        currentWave = waveNumber;
        Debug.Log("Starting Wave " + currentWave);

        SpawnWave();
    }

    void SpawnWave() {
        aliveMonsters.Clear();

        for (int i = 0; i < monstersPerWave; i++) {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject prefabToSpawn = normalPrefab;
            MonsterType type = MonsterType.NormalMonster;

            // Cứ 5 wave có Elite
            if (currentWave % 5 == 0 && i == monstersPerWave - 1) {
                prefabToSpawn = elitePrefab;
                type = MonsterType.Elite;
            }
            // Cứ 10 wave có BossMini
            if (currentWave % 10 == 0 && i == monstersPerWave - 1) {
                prefabToSpawn = bossMiniPrefab;
                type = MonsterType.BossMini;
            }
            // Cứ 20 wave có Boss
            if (currentWave % 20 == 0 && i == monstersPerWave - 1) {
                prefabToSpawn = bossPrefab;
                type = MonsterType.Boss;
            }

            GameObject monsterGO = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
            MonsterStats stats = monsterGO.GetComponent<MonsterStats>();
            stats.InitStats(type, currentWave); // level = số wave
            aliveMonsters.Add(stats);
        }
    }

    // Gọi khi quái chết
    public void OnMonsterDied(MonsterStats monster) {
        if (aliveMonsters.Contains(monster)) {
            aliveMonsters.Remove(monster);
        }

        if (aliveMonsters.Count == 0) {
            WaveCompleted();
        }
    }

    void WaveCompleted() {
        Debug.Log("Wave " + currentWave + " Completed!");
        // Gọi GameManager.Instance.OnWaveCompleted(currentWave);
    }

    public List<MonsterStats> GetAliveMonsters() {
        return aliveMonsters;
    }
}
