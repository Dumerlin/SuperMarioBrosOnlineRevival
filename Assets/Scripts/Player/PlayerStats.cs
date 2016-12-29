using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stats for the player.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    //Core stats
    [Header("Core Stats")]
    public int Level = 1;
    public int MaxHP = 5;
    public int HP = 5;
    public int MaxFP = 5;
    public int FP = 5;
    public int Attack = 0;
    public int Defense = 0;
    public int Speed = 0;
    public int Stache = 0;

    //Misc stats, with the exact value typically hidden from the player
    //Not all of these are finalized, so some may be added, removed, or modified
    [Header("Misc Stats")]
    public float ExpMod = 1f;
    public float DropChanceMod = 1f;
    public float HitRate = 0f;
    public float EvasionRate = 0f;
    public float CounterRate = 0f;

    /// <summary>
    /// Damages the player using a damage value and a specified element.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="element"></param>
    public void TakeDamage(int damage, Constants.DamageElements element)
    {
        //Placeholder; the total damage will need to be calculated later
        LoseHP(damage);
    }

    public void HealHP(int hp)
    {
        HP = Mathf.Clamp(HP + hp, 0, MaxHP);
    }

    /// <summary>
    /// Directly subtracts health from the player.
    /// </summary>
    /// <param name="hp"></param>
    public void LoseHP(int hp)
    {
        HP = Mathf.Clamp(HP - hp, 0, MaxHP);
    }

    public void HealFP(int fp)
    {
        FP = Mathf.Clamp(FP + fp, 0, MaxFP);
    }

    public void LoseFP(int fp)
    {
        FP = Mathf.Clamp(FP - fp, 0, MaxFP);
    }
}
