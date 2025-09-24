using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapTheme
{
    Grassland,
    Forest,
    Bridge,
    Village,
    Mountain,
    Cave,
    Volcano,
    Castle,
    Palace
}

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    [Header("Config Level")]
    public int currentLevel = 1;
    public int maxLevel = 10;

    [Header("Speed Settings")]
    public float baseSpeed = 5f;
    public float speedIncreasePerLevel = 0.1f; // 10% mỗi level
    private float currentSpeed;

    [Header("Obstacle Settings")]
    public int baseObstacleCount = 2;
    public int obstacleIncreasePerLevel = 2;
    public GameObject[] obstaclePrefabs;

    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;

    [Header("Map Theme Prefabs")]
    public GameObject[] mapThemes; // gán prefab theo enum MapTheme

    private GameObject currentMapTheme;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SetupLevel(currentLevel);
    }

    /// <summary>
    /// Khởi tạo level với các thông số (speed, obstacle, enemy, theme, boss nếu cần)
    /// </summary>
    public void SetupLevel(int level)
    {
        if (level < 1) level = 1;
        if (level > maxLevel) level = maxLevel;

        currentLevel = level;

        // Tính tốc độ
        currentSpeed = baseSpeed * (1 + speedIncreasePerLevel * (level - 1));

        // Spawn theme map
        SetupMapTheme(level);

        // Spawn obstacle
        int obstacleCount = baseObstacleCount + obstacleIncreasePerLevel * (level - 1);
        SpawnObstacles(obstacleCount);

        // Spawn enemy
        SpawnEnemies(level);

        // Boss ở Level 10
        if (level == 10 && bossPrefab != null)
        {
            SpawnBoss();
        }

        Debug.Log("MapManager: Setup Level " + level + " | Speed = " + currentSpeed);
    }

    /// <summary>
    /// Đổi theme map theo level
    /// </summary>
    void SetupMapTheme(int level)
    {
        if (currentMapTheme != null) Destroy(currentMapTheme);

        int themeIndex = Mathf.Clamp(level / 2, 0, mapThemes.Length - 1);
        currentMapTheme = Instantiate(mapThemes[themeIndex], Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// Sinh obstacle theo số lượng
    /// </summary>
    void SpawnObstacles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(Random.Range(5f, 20f) * i, 0, 0);
            int rand = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[rand], pos, Quaternion.identity);
        }
    }

    /// <summary>
    /// Sinh enemy tùy level
    /// </summary>
    void SpawnEnemies(int level)
    {
        int enemyCount = level + 1;

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(10f, 25f) * i, 0, 0);
            int rand = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[rand], pos, Quaternion.identity);
        }
    }

    /// <summary>
    /// Spawn boss khi đến level 10
    /// </summary>
    void SpawnBoss()
    {
        Vector3 pos = new Vector3(50f, 0, 0);
        Instantiate(bossPrefab, pos, Quaternion.identity);
    }

    /// <summary>
    /// Gọi sang level tiếp theo
    /// </summary>
    public void NextLevel()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            SetupLevel(currentLevel);
        }
        else
        {
            Debug.Log("Bạn đã hoàn thành tất cả level!");
        }
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
