using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {

	public Slider slider;      // Thanh máu
    public Gradient gradient;  // Màu chuyển dần (xanh → đỏ)
    public Image fill;         // Màu của thanh máu

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        if (fill != null)
            fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        if (fill != null)
            fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
