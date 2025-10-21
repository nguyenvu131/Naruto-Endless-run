using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour {
	
	public static BackToMainMenu Instance;
	
	// void Awake() {
        // if (Instance == null) {
            // Instance = this;
            // DontDestroyOnLoad(gameObject);
        // } else {
            // Destroy(gameObject);
        // }
    // }
	
	public void BackMainMenu() {
        // Reset TimeScale nếu game bị pause
        Time.timeScale = 1f;

        // Load Scene Main Menu
        SceneManager.LoadScene("newGame");
    }
}
