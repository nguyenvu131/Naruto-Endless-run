using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : MonoBehaviour {

	public void QuitGame() {
        Debug.Log("Thoát game!");
        Application.Quit(); 
    }
}
