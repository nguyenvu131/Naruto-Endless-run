using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {

	public Text coinText;

    void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        UpdateUI();
    }

    void UpdateUI()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if (coinText != null) coinText.text = "Coins: " + coins;
    }
}
