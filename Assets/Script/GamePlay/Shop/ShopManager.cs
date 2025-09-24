using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

	public ShopItem[] shopItems;

    public bool BuyItem(int index)
    {
        ShopItem item = shopItems[index];
        if (!item.isPurchased && ProgressionManager.Instance.SpendCoins(item.price))
        {
            item.isPurchased = true;
            Debug.Log("Đã mua: " + item.itemName);
            return true;
        }
        else
        {
            Debug.Log("Không đủ coin hoặc đã mua trước đó!");
            return false;
        }
    }
}
