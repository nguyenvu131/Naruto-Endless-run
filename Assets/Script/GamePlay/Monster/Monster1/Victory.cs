using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // cần cho LoadScene

public class Victory : MonoBehaviour {

	public string nextSceneName = "selectmap"; // tên scene sẽ chuyển tới khi Victory

    void Awake()
    {
        
    }

	// Hàm gọi khi player thắng
    public void OnVictory()
    {
        Debug.Log("Victory! Saving game...");

        SaveGameData(); // Lưu dữ liệu trước
        SceneManager.LoadScene(nextSceneName); // Chuyển scene sau
    }

    void SaveGameData()
    {
        
	
        // Lưu scene đã clear
        ES3.Save<string>("selectmap", SceneManager.GetActiveScene().name);
        Debug.Log("Game Saved with Easy Save 3!");
    }
}
