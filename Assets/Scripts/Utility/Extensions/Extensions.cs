﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extension methods for various classes.
/// </summary>
public static class Extensions
{
    #region GameObject Extensions

    /// <summary>
    /// GameObject extension to add a component if it's missing or get it if it exists.
    /// </summary>
    /// <typeparam name="T">A Component.</typeparam>
    /// <param name="gameObject">The GameObject.</param>
    /// <param name="component">The component.</param>
    /// <returns>A component of type T on the GameObject.</returns>
    public static T AddMissingComponent<T>(this GameObject gameObject, T component) where T: Component
    {
        T comp = gameObject.GetComponent<T>();
        if (comp != null) return comp;

        return gameObject.AddComponent<T>();
    }

    #endregion
}
