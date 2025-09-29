using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMapButton : MonoBehaviour {

	// Singleton để gọi từ mọi nơi
    public static SelectMapButton instance;
	
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
	
	public void GoToSelect() {
        SceneManager.LoadScene("selectmap");
    }
}
