using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour {

	public static SaveLoadSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private const string PLAYER_DATA_KEY = "player_data";

    // Lưu PlayerStats
    public void SavePlayerData(PlayerStats stats)
    {
        ES3.Save<PlayerStats>(PLAYER_DATA_KEY, stats);
        Debug.Log("✅ Player data saved!");
    }

    // Load PlayerStats
    public PlayerStats LoadPlayerData()
    {
        if (ES3.KeyExists(PLAYER_DATA_KEY))
        {
            PlayerStats loaded = ES3.Load<PlayerStats>(PLAYER_DATA_KEY);
            Debug.Log("✅ Player data loaded!");
            return loaded;
        }
        else
        {
            Debug.LogWarning("⚠️ No save file found. Creating new stats.");
            return null;
        }
    }

    // Xóa dữ liệu
    public void DeletePlayerData()
    {
        if (ES3.KeyExists(PLAYER_DATA_KEY))
        {
            ES3.DeleteKey(PLAYER_DATA_KEY);
            Debug.Log("🗑 Player data deleted!");
        }
    }
}