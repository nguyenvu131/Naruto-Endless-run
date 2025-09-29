using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{	
	public PlayerStats playerStats;
    public int coinValue = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddCoin(coinValue);
			// Lấy exp từ coin (ví dụ 10)
            float expAmount = 10f;

            playerStats.GainEXP(expAmount);
            Debug.Log("Picked up coin! Gained " + expAmount + " EXP.");
            Destroy(gameObject);
        }
    }
}
