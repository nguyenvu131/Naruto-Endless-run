using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] enemyPrefabs;    // Các loại enemy (Walker, Flyer...)
    public int poolSize = 10;            // Số enemy trong pool
    public float spawnInterval = 2f;     // Thời gian giữa các lần spawn
    public Transform player;             // Player để kiểm tra khoảng cách spawn
    public float spawnDistance = 15f;    // Spawn trước mặt player

    private List<GameObject> enemyPool;
    private float timer = 0f;

    void Start()
    {
        // Khởi tạo object pool
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject obj = Instantiate(enemyPrefabs[rand]);
            obj.SetActive(false);
            enemyPool.Add(obj);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Lấy enemy từ pool
        GameObject enemy = GetPooledEnemy();
        if (enemy == null) return;

        // Random vị trí spawn trước mặt player
        Vector3 spawnPos = new Vector3(
            player.position.x + spawnDistance,
            Random.Range(-1f, 0f), // Y random (trên/dưới đường chạy)
            0
        );

        enemy.transform.position = spawnPos;
        enemy.SetActive(true);
    }

    GameObject GetPooledEnemy()
    {
        foreach (GameObject obj in enemyPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null; // Nếu pool hết, không spawn
    }
}
