using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines various Constants.
/// </summary>
public static class Constants
{
    #region Enums

    /// <summary>
    /// The characters in the game.
    /// </summary>
    public enum Characters
    {
        Mario = 0, Luigi, Wario, Waluigi, Yoshi, Toad
    }

    /// <summary>
    /// The types of damage elements. Normal is the default for non-elemental moves.
    /// </summary>
    public enum DamageElements
    {
        Normal, Fire, Water, Earth, Wind, Ice, Electric, Poison, Explosion, Star
    }

    /// <summary>
    /// The various types of Currencies.
    /// </summary>
    public enum CurrencyTypes
    {
        Coins, StarPieces
    }
    
    /// <summary>
    /// The various types of items.
    /// </summary>
    public enum ItemTypes
    {
        Standard, Stackable, KeyItem
    }

    #endregion

    #region Fields

    //These apply to all currencies
    public const int MIN_CURRENCY = 0;
    public const int MAX_CURRENCY = 999999;

    public const int INV_START_SIZE = 20;
    public const int KEY_INV_MAX_SIZE = 999;

    #endregion
}
