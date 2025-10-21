using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "selectmap"; // Tên scene muốn load
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners(); // xóa listener cũ tránh bug miss
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}