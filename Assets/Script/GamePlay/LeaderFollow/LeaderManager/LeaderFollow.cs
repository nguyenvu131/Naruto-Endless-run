using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderFollow : MonoBehaviour {

	public LeaderData leaderData;
    public Transform player;
    public float followSpeed = 5f;
    public Vector3 offset = new Vector3(-1, 0, 0);
	public float horizontalSpacing = 1.5f; // khoảng cách giữa các leader
	
    [Header("UI Display")]
    public Text nameText;
    public Text levelText;
	
	[Header("Formation Settings")]
    public int formationIndex = 0;       // Vị trí của leader trong đội hình
    public int maxPerRow = 3;            // Số leader tối đa mỗi hàng
    public float rowSpacing = 1.0f;      // Khoảng cách hàng
    public float columnSpacing = 1.0f;   // Khoảng cách cột

	public int followIndex = 0; // Vị trí theo sau (0 = gần player nhất)
    public float followDistance = 1.5f; // Khoảng cách theo sau

    void Start()
    {
        if (leaderData == null) return;
        UpdateUI();
    }

    void Update()
    {
        if (player != null)
        {
            // Tính toán offset dựa trên formationIndex
            int row = formationIndex / maxPerRow;
            int col = formationIndex % maxPerRow;

            Vector3 formationOffset = new Vector3(
                offset.x - col * columnSpacing,
                offset.y - row * rowSpacing,
                offset.z
            );

            Vector3 targetPos = player.position + formationOffset;
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }
    }

    void UpdateUI()
    {
        if (nameText != null) nameText.text = leaderData.leaderName;
        if (levelText != null) levelText.text = "Lv." + leaderData.level;

    }
}
