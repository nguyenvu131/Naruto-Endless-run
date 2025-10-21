using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderShopManager : MonoBehaviour {

	public static LeaderShopManager Instance;
	public LeaderData[] allLeaders;        // Danh sách tất cả leader
    public Transform leaderUIParent;       // Nơi hiển thị UI Leader
    public GameObject leaderUIPrefab;      // Prefab UI item leader
	
	private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ dữ liệu khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
    private void Start()
    {
        PopulateShop();
    }

    void PopulateShop()
    {
        foreach (var leader in allLeaders)
        {
            GameObject uiItem = Instantiate(leaderUIPrefab, leaderUIParent);
            LeaderUI ui = uiItem.GetComponent<LeaderUI>();
            ui.SetLeaderData(leader);
        }
    }
}
