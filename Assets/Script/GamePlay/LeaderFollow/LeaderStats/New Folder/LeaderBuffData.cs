using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Game/SkillData")]
public class LeaderBuffData : ScriptableObject
{
    public string skillName;
    public Sprite skillIcon;
    public float cooldown;
    public float damage;
	public string buffName;
	public Sprite icon; // nếu muốn hiển thị icon
}
