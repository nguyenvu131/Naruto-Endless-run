using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int highScore = 0;
    private bool isGameOver = false;
    public Text coinText;
    protected int coins;

    public GameObject[] characterPrefabs;  // Prefab nhân vật
    [SerializeField] protected GameObject currentPlayer;

    void Awake()
    {
        // Singleton và DontDestroyOnLoad
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // giữ GameManager qua scene
        }
        else
        {
            Destroy(gameObject); // tránh tạo trùng
        }
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Spawn nhân vật đã chọn nếu đang ở gameplay
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Gameplay")
        {
            SpawnSelectedCharacter();
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            score += (int)(Time.deltaTime * 10); // tính điểm theo thời gian chạy
            if (UIManager.instance != null)
                UIManager.instance.UpdateScore(score);

            if (coinText != null)
                coinText.text = "Coins: " + coins;
        }
    }

    // Method để các script khác truy cập điểm
    public int GetScore()
    {
        return score;
    }

    public void AddCoin(int amount)
    {
        coins += amount;
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
}
