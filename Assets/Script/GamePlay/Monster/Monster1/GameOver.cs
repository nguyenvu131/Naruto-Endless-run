using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	
	public static GameOver Instance;
	protected PlayerStats stats;
	protected PlayerStatsSO statsSO;
	protected MonsterStatsSO monsterSO;
	protected MonsterStats monsterstats;
	
	void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
	
	public void BackGameOver() {
        // Load Scene Main Menu
        SceneManager.LoadScene("menu");
    }
}
