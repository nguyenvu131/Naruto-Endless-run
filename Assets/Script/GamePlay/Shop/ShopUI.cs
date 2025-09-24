using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {

	public Text coinText;
    public ShopManager shopManager;

    public Button[] buyButtons;

    void Start()
    {
        UpdateUI();

        for (int i = 0; i < buyButtons.Length; i++)
        {
            int index = i;
            buyButtons[i].onClick.AddListener(() => BuyItem(index));
        }
    }

    public void UpdateUI()
    {
        coinText.text = "Coins: " + ProgressionManager.Instance.playerData.coins;
    }

    public void BuyItem(int index)
    {
        bool success = shopManager.BuyItem(index);
        if (success) UpdateUI();
    }
}
