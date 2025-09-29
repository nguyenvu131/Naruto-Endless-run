using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

    public Text scoreText;
    public GameObject gameOverPanel;
    public Text finalScoreText;
    public Text highScoreText;
	
	public GameObject victoryPanel;
	
    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void ShowGameOver(int score, int highScore)
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Score: " + score;
        highScoreText.text = "Highscore: " + highScore;
    }
	
	public void ShowVictoryPanel()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);
    }
}
