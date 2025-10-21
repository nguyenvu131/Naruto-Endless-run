using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerStats playerStats;
    public PlayerStatsSO playerStatsSO;
    public int score = 0;
    public int highScore = 0;
    private bool isGameOver = false;
    public Text coinText;
    // protected int coins;

    public GameObject[] characterPrefabs;  // Prefab nhân vật
    [SerializeField] protected GameObject currentPlayer;

    void Awake()
    {
        // 🔹 Kiểm tra nếu đã có instance khác thì hủy object mới tạo
        if (Instance != null && Instance != this)
        {
            // Destroy(gameObject);
            return;
        }

        // 🔹 Gán instance và giữ lại khi đổi scene
        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Spawn nhân vật đã chọn nếu đang ở gameplay
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Gameplay")
        {
            SpawnSelectedCharacter();
        }
		// DontDestroyOnLoad(gameObject);
        
		LoadGame();
    }
  

    void Update()
    {
        if (!isGameOver)
        {
            score += (int)(Time.deltaTime * 10); // tính điểm theo thời gian chạy
            if (UIManager.instance != null)
                UIManager.instance.UpdateScore(score);

            if (coinText != null)
                coinText.text = "Coins: " + playerStats.coins;
        }
    }

    // Method để các script khác truy cập điểm
    public int GetScore()
    {
        return score;
    }

    public void AddCoin(int amount)
    {
        playerStats.coins += amount;
        score += amount; // nếu muốn coin cũng cộng score
        if (UIManager.instance != null)
            UIManager.instance.UpdateScore(score);
    }

    public void GameOver()
    {
        isGameOver = true;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        if (UIManager.instance != null)
            UIManager.instance.ShowGameOver(score, highScore);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    void SpawnSelectedCharacter()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        currentPlayer = Instantiate(characterPrefabs[selectedIndex], Vector3.zero, Quaternion.identity);
    }
	
	public void SaveGame()
    {
        SaveLoadSystem.Instance.SavePlayerData(playerStats);
    }

    public void LoadGame()
    {
        var loadedStats = SaveLoadSystem.Instance.LoadPlayerData();

        if (loadedStats != null)
        {
            playerStats = loadedStats;
        }
        else
        {
            playerStats = new PlayerStats();
            playerStats.CopyFromSO(playerStatsSO);
            SaveGame();
        }
    }

    // Gọi khi muốn reset game
    public void ResetGame()
    {
        SaveLoadSystem.Instance.DeletePlayerData();
        LoadGame();
    }
}
