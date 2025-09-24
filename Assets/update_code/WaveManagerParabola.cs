using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerParabola : MonoBehaviour {

	public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    [Header("Parabola Settings")]
    public float a = 0.5f; // Độ cong
    public float h = 0f;   // Đỉnh parabol (tọa độ x)
    public int k = 5;      // Số quái cơ bản

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaveLoop());
    }

    IEnumerator SpawnWaveLoop()
    {
        while (true)
        {
            currentWave++;

            int enemyCount = Mathf.RoundToInt(a * Mathf.Pow(currentWave - h, 2) + k);

            Debug.Log("Wave " + currentWave + " | EnemyCount = " + enemyCount);

            for (int i = 0; i < enemyCount; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

                yield return new WaitForSeconds(0.5f);
            }

            // Delay giữa các wave
            yield return new WaitForSeconds(3f);
        }
    }
}
