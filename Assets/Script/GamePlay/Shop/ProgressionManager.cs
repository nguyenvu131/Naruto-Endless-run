using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour {

	public static ProgressionManager Instance;

    public PlayerProgress playerData = new PlayerProgress();

    void Awake()
    {
        if (Instance == null) Instance = this;
        LoadProgress();
    }

    public void AddCoins(int amount)
    {
        playerData.coins += amount;
        SaveProgress();
    }

    public bool SpendCoins(int amount)
    {
        if (playerData.coins >= amount)
        {
            playerData.coins -= amount;
            SaveProgress();
            return true;
        }
        return false;
    }

    public void LevelUp()
    {
        playerData.playerLevel++;
        SaveProgress();
    }

    public void UpgradeHP()
    {
        playerData.hpLevel++;
        SaveProgress();
    }

    public void UpgradeDamage()
    {
        playerData.damageLevel++;
        SaveProgress();
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("Coins", playerData.coins);
        PlayerPrefs.SetInt("PlayerLevel", playerData.playerLevel);
        PlayerPrefs.SetInt("HPLevel", playerData.hpLevel);
        PlayerPrefs.SetInt("DamageLevel", playerData.damageLevel);
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        playerData.coins = PlayerPrefs.GetInt("Coins", 0);
        playerData.playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
        playerData.hpLevel = PlayerPrefs.GetInt("HPLevel", 1);
        playerData.damageLevel = PlayerPrefs.GetInt("DamageLevel", 1);
    }
}
