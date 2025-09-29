using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneContinueGame : MonoBehaviour {
	
	
	
	public void ContinueGame() {
        if (PlayerPrefs.HasKey("Scene")) {
            string scene = PlayerPrefs.GetString("Scene");
            SceneManager.LoadScene(scene);

            // Sau khi scene load xong thì set vị trí và stats
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else {
            Debug.Log("No save found!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) {
            Vector3 pos = new Vector3(
                PlayerPrefs.GetFloat("PosX"),
                PlayerPrefs.GetFloat("PosY"),
                PlayerPrefs.GetFloat("PosZ")
            );
            player.transform.position = pos;

            PlayerStats stats = player.GetComponent<PlayerStats>();
            if (stats != null) {
                // stats.HP = PlayerPrefs.GetInt("HP");
                // stats.Gold = PlayerPrefs.GetInt("Gold");
            }
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
