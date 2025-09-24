using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string characterID;    // ID (unique)
    public string characterName;  // Tên
    public int price;             // Giá coin
    public Sprite characterSprite;// Hình nhân vật
    public bool isUnlocked;       // Đã mở khóa chưa
}
