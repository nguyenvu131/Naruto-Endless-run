using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArmorFormula
{
    /// <summary>
    /// Tính damage cuối cùng sau khi trừ giáp
    /// </summary>
    /// <param name="rawDamage">Damage gốc</param>
    /// <param name="armor">Chỉ số giáp</param>
    /// <returns>Damage thực tế sau khi giảm bởi giáp</returns>
    public static int CalculateDamage(int rawDamage, int armor)
    {
        if (armor < 0) armor = 0; // không cho giáp âm
        float multiplier = 100f / (100f + armor);
        float finalDamage = rawDamage * multiplier;

        // Làm tròn để tránh damage = 0
        return Mathf.Max(1, Mathf.RoundToInt(finalDamage));
    }
}
