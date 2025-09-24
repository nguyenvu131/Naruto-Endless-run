using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public Transform player;              // Nhân vật
    public float spawnDistance = 20f;     // Khoảng cách spawn trước mặt player
    public float despawnDistance = 15f;   // Khoảng cách hủy phía sau player

    public GameObject[] segmentPrefabs;   // Các đoạn map (prefab)
    public int startSegments = 3;         // Số segment ban đầu

    private List<GameObject> activeSegments = new List<GameObject>();
    private float lastXPosition = 0f;     // Vị trí X để spawn segment tiếp theo
    private float segmentLength = 10f;    // Chiều dài mỗi segment (set theo prefab)

    void Start()
    {
        // Spawn sẵn map ban đầu
        for (int i = 0; i < startSegments; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        // Nếu player tiến gần cuối map -> spawn thêm
        if (player.position.x + spawnDistance > lastXPosition)
        {
            SpawnSegment();
        }

        // Xóa segment phía sau player
        if (activeSegments.Count > 0)
        {
            GameObject firstSegment = activeSegments[0];
            if (player.position.x - firstSegment.transform.position.x > despawnDistance)
            {
                Destroy(firstSegment);
                activeSegments.RemoveAt(0);
            }
        }
    }

    void SpawnSegment()
    {
        int rand = Random.Range(0, segmentPrefabs.Length);
        Vector3 spawnPos = new Vector3(lastXPosition, 0, 0);

        GameObject newSeg = Instantiate(segmentPrefabs[rand], spawnPos, Quaternion.identity);
        activeSegments.Add(newSeg);

        // Cập nhật vị trí X cho segment tiếp theo
        lastXPosition += segmentLength;
    }
}
