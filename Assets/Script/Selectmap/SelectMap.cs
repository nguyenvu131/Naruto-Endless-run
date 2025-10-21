using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour {
	
	
	// Singleton để gọi từ mọi nơi
    public static SelectMap instance;

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

    public void GoToSelectMap() {
        SceneManager.LoadScene("1 - 1");
    }
}
