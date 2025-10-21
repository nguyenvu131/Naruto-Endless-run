using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {

	[Header("UI References")]
    public Text coinText;
    public ShopManager shopManager;

    [Header("Buttons")]
    public Button[] buyButtons;

    void Start()
    {
        if (coinText == null)
        {
            Debug.LogWarning("Coin Text chưa gán!");
        }

        if (shopManager == null)
        {
            Debug.LogWarning("ShopManager chưa gán!");
            return;
        }

        // Gán listener cho button
        for (int i = 0; i < buyButtons.Length; i++)
        {
            if (buyButtons[i] == null) continue;

            int index = i; // capture index cho closure
            buyButtons[i].onClick.RemoveAllListeners();
            buyButtons[i].onClick.AddListener(() => BuyItem(index));
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (ProgressionManager.Instance == null || ProgressionManager.Instance.playerData == null)
        {
            Debug.LogWarning("ProgressionManager hoặc playerData null!");
            return;
        }

        if (coinText != null)
        {
            coinText.text = "Coins: " + ProgressionManager.Instance.playerData.coins;
        }

        // Cập nhật trạng thái button (disable nếu đã mua)
        if (shopManager != null && buyButtons != null)
        {
            for (int i = 0; i < buyButtons.Length; i++)
            {
                if (i >= shopManager.shopItems.Length) continue;
                if (buyButtons[i] == null) continue;

                buyButtons[i].interactable = !shopManager.shopItems[i].isPurchased;
            }
        }
    }

    public void BuyItem(int index)
    {
        if (shopManager == null) return;

        bool success = shopManager.BuyItem(index);
        if (success)
        {
            UpdateUI();
        }
    }
}
