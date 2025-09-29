using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public Boss boss;

    void Update()
    {
        if (boss != null && boss.statsSo != null)
        {
            slider.maxValue = boss.statsSo.maxHealth;
            slider.value = boss.GetCurrentHP();
        }
    }
}
