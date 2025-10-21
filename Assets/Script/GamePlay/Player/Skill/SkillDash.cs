using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : Skill
{
    public float dashSpeed = 15f;
    public float dashDuration = 2f;
    private PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    protected override void Activate()
    {
        Debug.Log("Ngựa Sắt Phi Nước Đại!");
        StartCoroutine(DashRoutine());
    }

    private System.Collections.IEnumerator DashRoutine()
    {
        float originalSpeed = player.moveSpeed;
        player.moveSpeed = dashSpeed;
        player.isInvincible = true;

        yield return new WaitForSeconds(dashDuration);

        player.moveSpeed = originalSpeed;
        player.isInvincible = false;
    }
}
