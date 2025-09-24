using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public float cooldown = 5f;
    private bool isReady = true;

    public Image cooldownImage; // UI hiển thị cooldown

    public void TryUseSkill()
    {
        if (isReady)
        {
            Activate();
            StartCoroutine(CooldownRoutine());
        }
    }

    protected abstract void Activate();

    private IEnumerator CooldownRoutine()
    {
        isReady = false;
        float timer = cooldown;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (cooldownImage != null)
                cooldownImage.fillAmount = timer / cooldown;
            yield return null;
        }
        isReady = true;
        if (cooldownImage != null)
            cooldownImage.fillAmount = 0;
    }
}
