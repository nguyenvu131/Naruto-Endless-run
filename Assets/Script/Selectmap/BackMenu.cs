using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour {
	
	// Singleton để gọi từ mọi nơi
    public static BackMenu instance;
	
	private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	public void BackToMainMenu() {
        Time.timeScale = 1f; // reset TimeScale nếu game đang pause
        SceneManager.LoadScene("menu"); // thay "MainMenu" bằng tên scene menu chính của bạn
    }
}
