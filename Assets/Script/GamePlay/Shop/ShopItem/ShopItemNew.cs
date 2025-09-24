using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemNew : MonoBehaviour {

	 public string itemName;        // Tên item
    public int price;              // Giá item
    public Button buyButton;       // Nút mua
    public Text priceText;         // Hiển thị giá
    public Text statusText;        // Hiển thị trạng thái (Đã mua / Chưa mua)

    private bool isPurchased = false;

    void Start()
    {
        // Load trạng thái từ PlayerPrefs
        isPurchased = PlayerPrefs.GetInt("Item_" + itemName, 0) == 1;

        priceText.text = price.ToString() + " Coins";
        UpdateUI();

        // Gán sự kiện cho nút
        buyButton.onClick.AddListener(BuyItem);
    }

    void BuyItem()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);

        if (!isPurchased && coins >= price)
        {
            coins -= price;
            PlayerPrefs.SetInt("Coins", coins);

            isPurchased = true;
            PlayerPrefs.SetInt("Item_" + itemName, 1);
            PlayerPrefs.Save();

            UpdateUI();
            Debug.Log(itemName + " đã được mua!");
        }
        else if (isPurchased)
        {
            Debug.Log("Item đã mua rồi!");
        }
        else
        {
            Debug.Log("Không đủ coins!");
        }
    }

    void UpdateUI()
    {
        if (isPurchased)
        {
            statusText.text = "Đã mua";
            buyButton.interactable = false;
        }
        else
        {
            statusText.text = "Chưa mua";
            buyButton.interactable = true;
        }
    }
}
