using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Singleton để gọi từ mọi nơi
    public static SceneLoader instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Load scene ngay lập tức theo tên
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
