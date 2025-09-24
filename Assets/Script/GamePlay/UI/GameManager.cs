using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

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
        if (instance == null) instance = this;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
		
		SpawnSelectedCharacter();
    }

    void Update()
    {
        if (!isGameOver)
        {
            score += (int)(Time.deltaTime * 10); // tính điểm theo thời gian chạy
            UIManager.instance.UpdateScore(score);
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
        score += amount;
        UIManager.instance.UpdateScore(score);
		coins += amount;
    }

    public void GameOver()
    {
        isGameOver = true;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

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
