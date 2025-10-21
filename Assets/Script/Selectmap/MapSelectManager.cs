using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelectManager : MonoBehaviour {

	[Header("Map Data")]
    public string[] mapSceneNames; // Tên scene của map (Map1, Map2...)
    public Sprite[] mapPreviews;   // Ảnh preview của từng map
    public string[] mapDescriptions; // Mô tả ngắn từng map

    [Header("UI References")]
    public Image previewImage;
    public Text mapNameText;
    public Text mapDescriptionText;
    public Button playButton;

    private int selectedMapIndex = 0;

    void Start()
    {
        // Load map đã chọn trước đó (nếu có)
        selectedMapIndex = PlayerPrefs.GetInt("SelectedMap", 0);
        UpdateMapPreview();
    }

    // Gọi khi nhấn nút chọn map
    public void SelectMap(int index)
    {
        if (index < 0 || index >= mapSceneNames.Length)
        {
            Debug.LogWarning("⚠️ Map index không hợp lệ!");
            return;
        }

        selectedMapIndex = index;
        PlayerPrefs.SetInt("SelectedMap", selectedMapIndex);
        PlayerPrefs.Save();
        UpdateMapPreview();
        Debug.Log("✅ Chọn map: " + mapSceneNames[index]);
    }

    // Cập nhật giao diện preview map
    void UpdateMapPreview()
    {
        if (previewImage && mapPreviews.Length > selectedMapIndex)
            previewImage.sprite = mapPreviews[selectedMapIndex];

        if (mapNameText)
            mapNameText.text = mapSceneNames[selectedMapIndex];

        if (mapDescriptionText && mapDescriptions.Length > selectedMapIndex)
            mapDescriptionText.text = mapDescriptions[selectedMapIndex];
    }

    // Khi nhấn nút "Play"
    public void PlaySelectedMap()
    {
        string mapToLoad = mapSceneNames[selectedMapIndex];
        Debug.Log("▶️ Loading Map: " + mapToLoad);
        SceneManager.LoadScene(mapToLoad);
    }

    // Quay lại Menu chính
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
