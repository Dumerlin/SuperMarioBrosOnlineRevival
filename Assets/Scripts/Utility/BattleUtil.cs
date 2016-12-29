using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility class for battle-related things, such as damage.
/// </summary>
public class BattleUtil
{
    /// <summary>
    /// Scalar parameter. The final result of the damage calculation is multiplied by this.
    /// </summary>
    public const float SCALAR = 0.5f;
    
    /// <summary>
    /// Tuning parameter.
    /// </summary>
    public const float TUNING = 1.5f;

    /// <summary>
    /// Maximum agency parameter.
    /// </summary>
    public const float MAX_AGENCY = 1.5f;

    //Kimimaru - NOTE: All current damage formulas are here. Only one will be chosen in the end after testing each

    public static int DamageFormula1Linear(PlayerStats attackerStats, PlayerStats victimStats)
    {
        return (int)((attackerStats.Attack - (TUNING * victimStats.Defense)) * SCALAR);
    }

    public static int DamageFormula2Quadratic(PlayerStats attackerStats, PlayerStats victimStats)
    {
        //Prevent division by 0
        int victimDef = victimStats.Defense;
        if (victimDef == 0) victimDef = 1;

        return (int)(((attackerStats.Attack * (attackerStats.Level + TUNING)) / victimDef) * SCALAR);
    }

    public static int DamageFormula3ExponentialDef(PlayerStats attackerStats, PlayerStats victimStats)
    {
        //Prevent division by 0
        if (attackerStats.Attack == 0) return 0;

        return (int)((attackerStats.Attack / (Mathf.Pow(TUNING, (victimStats.Defense / attackerStats.Attack)))) * SCALAR);
    }
}
