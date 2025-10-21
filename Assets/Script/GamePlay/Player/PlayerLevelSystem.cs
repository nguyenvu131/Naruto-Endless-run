using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    [Header("Tham chiếu ScriptableObject Stats")]
    public PlayerStatsSO playerStatsSO;

    [Header("Chỉ số runtime")]
    public PlayerStats stats;
	public UIManager uiManager;

    [Header("Exp & Level Up")]
    public float currentExp = 0;
	protected float currentLevel = 1;
    public int expToNextLevel = 10;   // Số coin cần để lên level
    public int expIncreasePerLevel = 5; // Tăng độ khó mỗi level
		
    void Start()
    {
        // Khởi tạo chỉ số ban đầu từ SO
        // stats = playerStatsSO.CreateRuntimeStats(playerStatsSO.baseLevel);
		currentLevel = stats.level;           // ⚠️ Dòng 21 (null nếu playerStats chưa gán)
        currentExp = stats.exp;
        //Debug.Log("Player Spawned with HP: " + runtimeStats.maxHP);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GainExp(1); // Mỗi coin = 1 exp (hoặc có thể coin.value)
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Thêm exp khi nhặt coin
    /// </summary>
    public void GainExp(int amount)
    {
        currentExp += amount;
        Debug.Log("Exp: " + currentExp + "/" + expToNextLevel);

        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// Lên level + cập nhật chỉ số
    /// </summary>
    public void LevelUp()
    {
        currentExp -= expToNextLevel;   // Trừ exp đã dùng
        stats.level++;
        expToNextLevel += expIncreasePerLevel;

        // Cập nhật chỉ số mới
        stats = playerStatsSO.CreateRuntimeStats(stats.level);

        Debug.Log("Level Up! Level: " + stats.level + " | HP: " + stats.maxHP + " | ATK: " + stats.attack);
    }
}
