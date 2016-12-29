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
    public int HP = 0;
    public int FP = 0;
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
}
