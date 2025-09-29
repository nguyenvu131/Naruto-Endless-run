using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackShop : MonoBehaviour {
	
	public static BackShop Instance;
	
	void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
	
	public void BackShopUI() {
        // Reset TimeScale nếu game bị pause
        // Time.timeScale = 1f;

        // Load Scene Main Menu
        SceneManager.LoadScene("menu");
    }
}
