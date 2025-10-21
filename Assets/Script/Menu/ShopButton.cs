using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopButton : MonoBehaviour 
{	
	public static ShopButton Instance;
	
	void Awake() {
        if (Instance == null) {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
	
	public void GoToShop() {
        SceneManager.LoadScene("shop");
    }
}
