using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General Utility class.
/// </summary>
public static class Util
{
    /// <summary>
    /// Clamps a value between a min uint value and a max uint value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static uint Clamp(uint value, uint min, uint max)
    {
        return value < min ? min : value > max ? max : value;
    }
}
