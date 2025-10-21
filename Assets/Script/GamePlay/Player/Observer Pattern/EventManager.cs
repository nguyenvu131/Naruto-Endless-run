using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {

	// Sự kiện ăn coin
    public static Action<int> OnCoinCollected;

    // Sự kiện va chạm kẻ địch
    public static Action<float> OnPlayerHit;

    // Sự kiện người chơi chết
    public static Action OnPlayerDead;

    // Sự kiện milestone (ví dụ đạt X mét)
    public static Action<int> OnMilestoneReached;

    // Sự kiện ăn power-up
    public static Action<string> OnPowerUpCollected;

    // Hàm reset để tránh memory leak (khi chuyển scene)
    public static void Reset()
    {
        OnCoinCollected = null;
        OnPlayerHit = null;
        OnPlayerDead = null;
        OnMilestoneReached = null;
        OnPowerUpCollected = null;
    }
}
