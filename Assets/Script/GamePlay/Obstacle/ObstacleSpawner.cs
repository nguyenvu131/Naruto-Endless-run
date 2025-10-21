using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject[] obstaclePrefabs;
    public float spawnInterval = 2f;
    public Transform spawnPoint;

    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int rand = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[rand], spawnPoint.position, Quaternion.identity);
        }
    }
}
