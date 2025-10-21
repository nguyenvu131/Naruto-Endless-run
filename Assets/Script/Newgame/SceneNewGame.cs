using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNewGame : MonoBehaviour {

	public static SceneNewGame Instance;

//    void Awake() {
//        if (Instance == null) {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        } else {
//            Destroy(gameObject);
//        }
//    }

    // Gọi khi bấm New Game
    public void NewGame() {
        ResetData();
        SceneManager.LoadScene("menu"); // tên scene đầu tiên
    }

    // Reset toàn bộ dữ liệu về mặc định
    private void ResetData() {
        PlayerPrefs.DeleteAll(); // xoá dữ liệu cũ

        // Hoặc set giá trị mặc định mới
        PlayerPrefs.SetInt("PlayerHP", 100);
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("PlayerExp", 0);
        PlayerPrefs.SetInt("PlayerGold", 0);

        PlayerPrefs.Save();
    }
}
