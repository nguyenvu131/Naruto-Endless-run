using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

	public GameObject coinPrefab;
    public float spawnInterval = 1.5f;
    public Transform spawnPoint;

    void Start()
    {
        StartCoroutine(SpawnCoin());
    }

    IEnumerator SpawnCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Vector3 pos = spawnPoint.position + new Vector3(0, Random.Range(1f, 2f), 0);
            Instantiate(coinPrefab, pos, Quaternion.identity);
        }
    }
}
