using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour {
    public static PlayerStatsManager Instance; // Singleton

    public PlayerStats playerStats;

    // UI References
    [Header("UI References")]
    public Slider hpSlider;
    // public Slider mpSlider;
    // public Slider expSlider;

    public Text hpText;
    public Text mpText;
    public Text expText;
    public Text levelText;

	public int level = 1;
    public int baseDamage = 5;
	
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        if (playerStats == null) {
            playerStats = new PlayerStats(); // Tạo stats mặc định
        }

        // Khởi tạo UI ban đầu
        UpdateAllUI();
    }

    void Update() {
        // Cập nhật UI mỗi frame (có thể tối ưu bằng event)
        UpdateAllUI();
    }

    public void UpdateAllUI() {
        if (hpSlider != null) {
            hpSlider.maxValue = playerStats.maxHP;
            hpSlider.value = playerStats.currentHP;
        }

        // if (mpSlider != null) {
            // mpSlider.maxValue = playerStats.maxMP;
            // mpSlider.value = playerStats.currentMP;
        // }

        // if (expSlider != null) {
            // expSlider.maxValue = playerStats.expToNextLevel;
            // expSlider.value = playerStats.exp;
        // }

        if (hpText != null) {
            hpText.text = "HP: " + playerStats.currentHP + "/" + playerStats.maxHP;
        }

        if (mpText != null) {
            mpText.text = "MP: " + playerStats.currentMP + "/" + playerStats.maxMP;
        }

        if (expText != null) {
            expText.text = "EXP: " + playerStats.exp + "/" + playerStats.expToNextLevel;
        }

        if (levelText != null) {
            levelText.text = "Level: " + playerStats.level;
        }
    }

    // --- Các hàm tiện ích để gọi từ gameplay ---

    public void PlayerTakeDamage(float enemyATK) {
        playerStats.TakeDamage(enemyATK);
        UpdateAllUI();
    }

    public void PlayerHeal(float amount) {
        playerStats.Heal(amount);
        UpdateAllUI();
    }

    public void PlayerGainEXP(float amount) {
        playerStats.GainEXP(amount);
        UpdateAllUI();
    }

    public float PlayerAttack() {
        return playerStats.CalculateDamage();
    }
	
	public int GetDamage()
    {
        // Damage tăng theo level (ví dụ công thức đơn giản)
        return baseDamage + level * 2;
    }
}
