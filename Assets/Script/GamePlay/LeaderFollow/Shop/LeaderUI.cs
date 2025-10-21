using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderUI : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text priceText;
    public Button buyButton;

    private LeaderData leader;

    public void SetLeaderData(LeaderData data)
    {
        leader = data;
        icon.sprite = data.icon;
        nameText.text = data.leaderName;
        priceText.text = data.isPurchased ? "Owned" : data.price.ToString();

        buyButton.interactable = !data.isPurchased;
        buyButton.onClick.AddListener(OnBuyClicked);
    }

    void OnBuyClicked()
    {
        if (!leader.isPurchased)
        {
            // Kiểm tra tiền người chơi
            int playerCoins = PlayerPrefs.GetInt("Coins", 0);
            if (playerCoins >= leader.price)
            {
                playerCoins -= leader.price;
                PlayerPrefs.SetInt("Coins", playerCoins);

                leader.isPurchased = true;
                priceText.text = "Owned";
                buyButton.interactable = false;

                Debug.Log("{leader.leaderName} purchased!");
				
				// Spawn leader theo player
				if (LeaderManager.Instance != null)
				{
					LeaderManager.Instance.AddLeader(leader);
				}
				
				// Lưu dữ liệu leader đã mua
                SavePurchasedLeader(leader);

            }
            else
            {
                Debug.Log("Not enough coins!");
            }
        }
    }
	
	void SavePurchasedLeader(LeaderData leader)
		{
			string savedLeaders = PlayerPrefs.GetString("PurchasedLeaders", "");
			List<string> leaderList = new List<string>();

			if (!string.IsNullOrEmpty(savedLeaders))
				leaderList = new List<string>(savedLeaders.Split(','));

			if (!leaderList.Contains(leader.leaderName))
				leaderList.Add(leader.leaderName);

			// Chuyển List<string> sang array để string.Join hợp lệ
			PlayerPrefs.SetString("PurchasedLeaders", string.Join(",", leaderList.ToArray()));
			PlayerPrefs.Save();
		}

}
