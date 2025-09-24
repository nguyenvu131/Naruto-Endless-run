using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerNew : MonoBehaviour {

	public Text coinText;   // Hiển thị số coin hiện có

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        coinText.text = "Coins: " + coins;
    }

    // Hàm cộng coin (test)
    public void AddCoins(int amount)
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
        UpdateUI();
    }
}
